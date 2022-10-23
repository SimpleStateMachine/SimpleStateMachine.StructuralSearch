using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    internal static class FindTemplateParser
    {
        static FindTemplateParser()
        {
            Parenthesised = Parsers.BetweenOneOfChars(x => ParserToParser.CIChar(x)
                    .Select(x => x.AsString()),
                Parser.Rec(() => Term),
                Constant.AllParenthesised);

            Term = Parser.OneOf(Parenthesised, Token)
                .Many()
                .MergerMany();

            TemplateParser = Parser.OneOf(Parenthesised, Token)
                .AtLeastOnceUntil(CommonParser.EOF)
                .MergerMany();

            SeriesParser = TemplateParser.Select(parsers => new SeriesParser(parsers));
        }
        
        internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Empty =
            ParserToParser.ResultAsParser(CommonParser.Empty)
                .AsMany();  
        
        internal static readonly Parser<char, Parser<char, string>> AnyString =
            ParserToParser.ResultAsParser(CommonParser.AnyString)
                .Try();
            
        internal static readonly Parser<char, Parser<char, string>> WhiteSpaces =
            ParserToParser.ParserAsParser(CommonParser.WhiteSpaces)
                .Try();  
        
        internal static readonly Parser<char, Parser<char, string>> Placeholder = 
            CommonTemplateParser.Placeholder
                .Select(name => new PlaceholderParser(name))
                .As<char, PlaceholderParser, Parser<char, string>>(); 
        
        internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Token =
            Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
                .AsMany(); 
        
        internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Term;
        
        internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Parenthesised;
        
        private static readonly Parser<char, IEnumerable<Parser<char, string>>> TemplateParser;
        
        private static readonly Parser<char, SeriesParser> SeriesParser;
        
        internal static IFindParser ParseTemplate(string? str)
        {
            return string.IsNullOrEmpty(str)
                ? FindParser.Empty 
                : SeriesParser.Select(parser => new FindParser(parser)).ParseOrThrow(str);
        }
    }
}