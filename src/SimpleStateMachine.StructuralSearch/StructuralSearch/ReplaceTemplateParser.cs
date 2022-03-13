using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceTemplateParser
    {
        static ReplaceTemplateParser()
        {
            Parenthesised = Parsers.BetweenOneOfChars(x=> ParserToReplace.Stringc(x),
                Parser.Rec(() => Term),
                Constant.AllParenthesised);

            Term = Parser.OneOf(Parenthesised, Token)
                .Many()
                .MergerMany();

            TemplateParser = Parser.OneOf(Parenthesised, Token)
                .AtLeastOnce()
                .MergerMany();
        }

        public static readonly Parser<char, IReplaceStep> Empty = 
            ParserToReplace.ResultAsReplace(CommonParser.Empty);

        public static readonly Parser<char, IReplaceStep> AnyString = 
            ParserToReplace.ResultAsReplace(CommonParser.AnyString)
                .Try();

        public static readonly Parser<char, IReplaceStep> WhiteSpaces =
            ParserToReplace.ResultAsReplace(CommonParser.WhiteSpaces)
                .Try();

        public static readonly Parser<char, IReplaceStep> Placeholder =
            CommonTemplateParser.Placeholder.Select(name=> new PlaceholderReplace(name))
                .As<char, PlaceholderReplace, IReplaceStep>()
                .Try();

        public static readonly Parser<char, IEnumerable<IReplaceStep>> Token =
            Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
                .AsMany();

        public static readonly Parser<char, IEnumerable<IReplaceStep>> Term;

        public static readonly Parser<char, IEnumerable<IReplaceStep>> Parenthesised;

        private static readonly Parser<char, IEnumerable<IReplaceStep>> TemplateParser;

        internal static IReplaceBuilder ParseTemplate(string str)
        {
            return TemplateParser
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);
        }
    }
}