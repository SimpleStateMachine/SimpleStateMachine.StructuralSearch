using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsers;
using SimpleStateMachine.StructuralSearch.Templates.FindTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindTemplateParser
{
    private static readonly Parser<char, Parser<char, string>> AnyString =
        ParserToParser.ResultAsParser(CommonParser.AnyString).Try();

    private static readonly Parser<char, Parser<char, string>> WhiteSpaces =
        ParserToParser.ParserAsParser(CommonParser.WhiteSpaces).Try();

    private static readonly Parser<char, Parser<char, string>> Placeholder =
        CommonTemplateParser.Placeholder.Select(name => new PlaceholderParser(name))
            .As<char, PlaceholderParser, Parser<char, string>>();

    private static readonly Parser<char, IEnumerable<Parser<char, string>>> Token =
        Parser.OneOf(AnyString, Placeholder, WhiteSpaces).AsMany();

    private static readonly Parser<char, IEnumerable<Parser<char, string>>> Parenthesised =
        Parsers.Parsers.BetweenOneOfChars
        (
            leftRight: x => ParserToParser.CiChar(x).Select(p => p.AsString()),
            expr: Parser.Rec(() => Term ?? throw new ArgumentNullException(nameof(Term))),
            values: Constant.AllParentheses
        );

    internal static Parser<char, IEnumerable<Parser<char, string>>> Term =
        Parser.OneOf(Parenthesised, Token).Many().SelectMany();

    private static readonly Parser<char, IEnumerable<Parser<char, string>>> TemplateParser =
        Parser.OneOf(Parenthesised, Token).AtLeastOnceUntil(CommonParser.Eof).SelectMany();

    private static readonly Parser<char, SeriesParser> SeriesParser =
        TemplateParser.Select(parsers => new SeriesParser(parsers));

    internal static IFindParser ParseTemplate(string? str) =>
        string.IsNullOrEmpty(str)
            ? EmptyFindParser.Value
            : SeriesParser.Select(parser => new FindParser(parser)).ParseOrThrow(str);
}