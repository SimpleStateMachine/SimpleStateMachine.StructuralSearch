using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Sandbox.Custom;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class FindTemplateParser
    {
        static FindTemplateParser()
        {
            Parenthesised = Parsers.BetweenOneOfChars(
                ParserToParser.StringcMatch, 
                Parser.Rec(() => Term), 
                Constant.AllParenthesised);
            
            Term = Parser.OneOf(Parenthesised, Token.AtLeastOnce())
                .AtLeastOnce()
                .MergerMany();

            TemplateParser = Term
                .Select(x => Parsers.Series(x, enumerable => enumerable.Concatenate()));
        }
        
        public static readonly Parser<char, Parser<char, SourceMatch>> AnyString =
            ParserToParser.ResultAsMatch(CommonParser.AnyString).Try();
            
        public static readonly Parser<char, Parser<char, SourceMatch>> WhiteSpaces =
            ParserToParser.ResultAsMatch(CommonParser.WhiteSpaces).Try();  
        
        public static readonly Parser<char, Parser<char, SourceMatch>> Placeholder = 
            CommonTemplateParser.Placeholder
                .Select(name => new PlaceholderParser(name))
                .Cast<Parser<char, SourceMatch>>(); 
        
        public static readonly Parser<char, Parser<char, SourceMatch>> Token =
            Parser.OneOf(AnyString, Placeholder, WhiteSpaces); 
        
        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Term;

        public static readonly Parser<char, IEnumerable<Parser<char, SourceMatch>>> Parenthesised;
        
        private static readonly Parser<char, Parser<char, SourceMatch>> TemplateParser;

        public static Parser<char, SourceMatch> ParseTemplate(string str)
        {
            return TemplateParser.ParseOrThrow(str);
        }
    }
}