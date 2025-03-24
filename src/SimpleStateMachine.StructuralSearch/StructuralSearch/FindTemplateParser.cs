using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsers;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindTemplateParser
{
    private static readonly Parser<char, Parser<char, string>> Placeholder =
        Grammar.Placeholder.Select(Parser<char, string> (name) => new PlaceholderParser(name));

    private static readonly Parser<char, Parser<char, string>> StringLiteral =
        Grammar.StringLiteral.SelectToParser((value, _) => Parser.String(value)).Try();

    private static readonly Parser<char, Parser<char, string>> WhiteSpaces =
        Grammar.WhiteSpaces.SelectToParser((_, parser) => parser).Try();

    private static readonly Parser<char, Parser<char, string>> Token =
        Parser.OneOf(Placeholder, StringLiteral, WhiteSpaces);

    private static readonly Parser<char, Parser<char, string>> Parenthesised =
        Parsers.Parsers.BetweenParentheses(Parser.Rec(() => Term ?? throw new ArgumentNullException(nameof(Term))), (_, result, _) => result);

    // HOw to handle many?
    private static readonly Parser<char, Parser<char, string>> Term =
        Parser.OneOf(Parenthesised, Token);

    private static readonly Parser<char, IEnumerable<Parser<char, string>>> TemplateParsers =
        Parser.OneOf(Parenthesised, Token).AsMany().AtLeastOnceUntil(CommonParser.Eof).SelectMany();

    private static readonly Parser<char, Parsers.FindTemplateParser> TemplateParser =
        TemplateParsers.Select(parsers => new Parsers.FindTemplateParser(parsers));

    internal static IFindParser ParseTemplate(string? str) =>
        string.IsNullOrEmpty(str)
            ? EmptyFindParser.Value
            : TemplateParser.Select(parser => new FindParser(parser)).ParseOrThrow(str);
}