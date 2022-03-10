using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public static class FindTemplateParser
    {
        //TODO return string for all parsers exclude  TemplateParser
        static FindTemplateParser()
        {
            Parenthesised = Parsers.BetweenOneOfChars(ParserToParser.StringcMatch,
                Parser.Rec(() => Term),
                Constant.AllParenthesised);

            Term = Parser.OneOf(Parenthesised, Token)
                .Many()
                .MergerMany();

            TemplateParser = Parser.OneOf(Parenthesised, Token)
                .AtLeastOnce()
                .MergerMany()
                .JoinResults();
        }
        
        
        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Empty =
            ParserToParser.ResultAsMatch(CommonParser.Empty)
                .AsMany();  
        
        public static readonly Parser<char, Parser<char, SourceMatch>> AnyString =
            ParserToParser.ResultAsMatch(CommonParser.AnyString)
                .Try();
            
        public static readonly Parser<char, Parser<char, SourceMatch>> WhiteSpaces =
            ParserToParser.ResultAsMatch(CommonParser.WhiteSpaces)
                .Try();  
        
        public static readonly Parser<char, Parser<char, SourceMatch>> Placeholder = 
            CommonTemplateParser.Placeholder
                .Select(name => new PlaceholderParser(name))
                .Cast<Parser<char, SourceMatch>>(); 
        
        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Token =
            Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
                .AsMany(); 
        
        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Term;
        
        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Parenthesised;
        
        private static readonly Parser<char, Parser<char, SourceMatch>> TemplateParser;
        
        internal static Parser<char, SourceMatch> ParseTemplate(string str)
        {
            return TemplateParser.ParseOrThrow(str);
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
        
        
        // public static readonly Parser<char, Parser<char, SourceMatch>> Empty =
        //     ParserToParser.ResultAsMatch(CommonParser.Empty);  
        //
        // public static readonly Parser<char, Parser<char, SourceMatch>> AnyString =
        //     ParserToParser.ResultAsMatch(CommonParser.AnyString).Try();
        //     
        // public static readonly Parser<char, Parser<char, SourceMatch>> WhiteSpaces =
        //     ParserToParser.ResultAsMatch(CommonParser.WhiteSpaces).Try();  
        //
        // public static readonly Parser<char, Parser<char, SourceMatch>> Placeholder = 
        //     CommonTemplateParser.Placeholder
        //         .Select(name => new PlaceholderParser(name))
        //         .Cast<Parser<char, SourceMatch>>(); 
        //
        // public static readonly Parser<char, Parser<char, SourceMatch>> Token =
        //     Parser.OneOf(AnyString, Placeholder, WhiteSpaces); 
        //
        // public static readonly Parser<char, Parser<char, SourceMatch>> Term;
        //
        // public static readonly Parser<char, Parser<char, SourceMatch>> Parenthesised;
        //
        // private static readonly Parser<char, Parser<char, SourceMatch>> TemplateParser;
        //
        // internal static Parser<char, SourceMatch> ParseTemplate(string str)
        // {
        //     return TemplateParser.ParseOrThrow(str);
        // }
    }
}