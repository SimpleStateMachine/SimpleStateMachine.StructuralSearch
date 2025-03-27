using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindTemplateParser
{
    private static readonly Parser<char, Parser<char, string>> Placeholder =
        Grammar.Placeholder.Select(Parser<char, string> (name) => new PlaceholderParser(name));

    private static readonly Parser<char, Parser<char, string>> StringLiteral =
        Grammar.StringLiteral.SelectToParser((value, _) => Parser.String(value));

    private static readonly Parser<char, Parser<char, string>> WhiteSpaces =
        Grammar.WhiteSpaces.SelectToParser((_, parser) => parser);

    private static readonly Parser<char, Parser<char, string>> Token =
        Parser.OneOf(Placeholder, StringLiteral, WhiteSpaces);

    private static readonly Parser<char, IEnumerable<Parser<char, string>>> TokenInParentheses =
        Parsers.BetweenParentheses
        (
            expr: Parser.OneOf
            (
                // Recursive
                Parser.Rec(() => TokenInParentheses ?? throw new ArgumentNullException(nameof(TokenInParentheses))),
                Token.AtLeastOnce()
            ),
            // Merge parsers
            mapFunc: (left, result, right) =>
            {
                var leftParser = Parser.Char(left).AsString();
                var rightParser = Parser.Char(right).AsString();
                return result.Prepend(leftParser).Append(rightParser);
            }
        );

    private static readonly Parser<char, CustomParsers.FindTemplateParser> Term =
        Parser.OneOf(TokenInParentheses, Token.AsMany())
            .AtLeastOnceUntil(CommonParser.Eof)
            .Select(x => x.SelectMany(y => y))
            .Select(parsers => new CustomParsers.FindTemplateParser(parsers.ToList()));

    internal static IFindParser ParseTemplate(string? str) =>
        string.IsNullOrEmpty(str)
            ? EmptyFindParser.Value
            : Term.Select(parser => new FindParser(parser)).ParseOrThrow(str);
}