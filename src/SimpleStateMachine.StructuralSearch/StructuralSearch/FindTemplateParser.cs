using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    internal static class FindTemplateParser
    {
        internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Empty =
            ParserToParser.ResultAsParser(CommonParser.Empty)
                .AsMany();  
        
        internal static readonly Parser<char, Parser<char, string>> AnyString =
            ParserToParser.ResultAsParser(CommonParser.AnyString)
                .Try();
            
        internal static readonly Parser<char, Parser<char, string>> WhiteSpaces =
            ParserToParser.ParserAsParser(CommonParser.WhiteSpaces)
                .Try();  
        
        internal static Parser<char, Parser<char, string>> Placeholder(IReadOnlyList<IFindRule> findRules)
            => CommonTemplateParser.Placeholder
                .Select(name =>
                {
                    var rules= findRules.Where(r => r.IsApplicableForPlaceholder(name)).ToList().AsReadOnly();
                    return new PlaceholderParser(name, rules);
                }).As<char, PlaceholderParser, Parser<char, string>>(); 
        
        internal static Parser<char, IEnumerable<Parser<char, string>>> Token(IReadOnlyList<IFindRule> findRules)
            => Parser.OneOf(AnyString, Placeholder(findRules), WhiteSpaces).AsMany();

        internal static Parser<char, IEnumerable<Parser<char, string>>> Term(IReadOnlyList<IFindRule> findRules)
            => Parser.OneOf(Parenthesised(findRules), Token(findRules)).Many().MergerMany();
        
        internal static Parser<char, IEnumerable<Parser<char, string>>> Parenthesised(IReadOnlyList<IFindRule> findRules)
            => Parsers.BetweenOneOfChars(x => ParserToParser.CIChar(x).Select(x => x.AsString()),
                Parser.Rec(() => Term(findRules) ?? throw new ArgumentNullException(nameof(Term))),
                Constant.AllParenthesised);
        
        private static Parser<char, IEnumerable<Parser<char, string>>> TemplateParser(IReadOnlyList<IFindRule> findRules)
            => Parser.OneOf(Parenthesised(findRules), Token(findRules)).AtLeastOnceUntil(CommonParser.EOF).MergerMany();
        
        private static Parser<char, SeriesParser> SeriesParser(IReadOnlyList<IFindRule> findRules)
            => TemplateParser(findRules).Select(parsers => new SeriesParser(parsers));
        
        internal static IFindParser ParseTemplate(string? str, IReadOnlyList<IFindRule> findRules)
            => string.IsNullOrEmpty(str)
                ? FindParser.Empty 
                : SeriesParser(findRules).Select(parser => new FindParser(parser)).ParseOrThrow(str);
    }
}