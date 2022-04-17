using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    internal static class FindTemplateParser
    {
        static FindTemplateParser()
        {
            Parenthesised = Parsers.BetweenOneOfChars(x => ParserToParser.Stringc(x),
                Parser.Rec(() => Term),
                Constant.AllParenthesised);

            Term = Parser.OneOf(Parenthesised, Token)
                .Many()
                .MergerMany();

            TemplateParser = Parser.OneOf(Parenthesised, Token)
                .AtLeastOnce()
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
        
        internal static IFindParser ParseTemplate(string str)
        {
            return SeriesParser.Select(parser => new FindParser(parser)).ParseOrThrow(str);
        }
        
        
        
        
        // static FindTemplateParser()
        // {
        //     Parenthesised = Parsers.BetweenOneOfChars(ParserToParser.StringcMatch,
        //         Parser.Rec(() => Term.Or(Empty)),
        //         Constant.AllParenthesised);
        //
        //     Term = Parser.OneOf(Parenthesised, Token)
        //         .AtLeastOnce();
        //
        //     
        //     TemplateParser = Term;
        // }
        
        
        // internal static readonly Parser<char, Parser<char, SourceMatch>> Empty =
        //     ParserToParser.ResultAsMatch(CommonParser.Empty);  
        //
        // internal static readonly Parser<char, Parser<char, SourceMatch>> AnyString =
        //     ParserToParser.ResultAsMatch(CommonParser.AnyString).Try();
        //     
        // internal static readonly Parser<char, Parser<char, SourceMatch>> WhiteSpaces =
        //     ParserToParser.ResultAsMatch(CommonParser.WhiteSpaces).Try();  
        //
        // internal static readonly Parser<char, Parser<char, SourceMatch>> Placeholder = 
        //     CommonTemplateParser.Placeholder
        //         .Select(name => new PlaceholderParser(name))
        //         .Cast<Parser<char, SourceMatch>>(); 
        //
        // internal static readonly Parser<char, Parser<char, SourceMatch>> Token =
        //     Parser.OneOf(AnyString, Placeholder, WhiteSpaces); 
        //
        // internal static readonly Parser<char, Parser<char, SourceMatch>> Term;
        //
        // internal static readonly Parser<char, Parser<char, SourceMatch>> Parenthesised;
        //
        // private static readonly Parser<char, Parser<char, SourceMatch>> TemplateParser;
        //
        // internal static Parser<char, SourceMatch> ParseTemplate(string str)
        // {
        //     return TemplateParser.ParseOrThrow(str);
        // }
    }
}