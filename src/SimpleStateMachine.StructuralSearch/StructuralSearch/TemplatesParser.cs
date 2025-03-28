using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class TemplatesParser
{
    private static readonly Parser<char, Parser<char, string>> Placeholder =
        Grammar.Placeholder.Select(Parser<char, string> (name) => new PlaceholderParser(name));

    private static readonly Parser<char, Parser<char, string>> StringLiteral =
        Grammar.StringLiteral.SelectToParser((value, _) => Parser.String(value));

    private static readonly Parser<char, Parser<char, string>> WhiteSpaces =
        Grammar.WhiteSpaces.SelectToParser((_, parser) => parser);

    internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Template =
        Parsers.BetweenParentheses
        (
            expr: Parser.OneOf
            (
                // Recursive
                Parser.Rec(() => Template ?? throw new ArgumentNullException(nameof(Template))),
                Parser.OneOf(Placeholder, StringLiteral, WhiteSpaces).AtLeastOnce()
            ),
            // Merge parsers
            mapFunc: (left, result, right) =>
            {
                var leftParser = Parser.Char(left).AsString();
                var rightParser = Parser.Char(right).AsString();
                return result.Prepend(leftParser).Append(rightParser);
            }
        );
}