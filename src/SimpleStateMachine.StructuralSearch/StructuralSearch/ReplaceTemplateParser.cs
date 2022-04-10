using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    internal class ReplaceTemplateParser
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

        internal static readonly Parser<char, IReplaceStep> Empty = 
            ParserToReplace.ResultAsReplace(CommonParser.Empty);

        internal static readonly Parser<char, IReplaceStep> AnyString = 
            ParserToReplace.ResultAsReplace(CommonParser.AnyString)
                .Try();

        internal static readonly Parser<char, IReplaceStep> WhiteSpaces =
            ParserToReplace.ResultAsReplace(CommonParser.WhiteSpaces)
                .Try();

        internal static readonly Parser<char, IReplaceStep> Placeholder =
            CommonTemplateParser.Placeholder.Select(name=> new PlaceholderReplace(name))
                .As<char, PlaceholderReplace, IReplaceStep>()
                .Try();

        internal static readonly Parser<char, IEnumerable<IReplaceStep>> Token =
            Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
                .AsMany();

        internal static readonly Parser<char, IEnumerable<IReplaceStep>> Term;

        internal static readonly Parser<char, IEnumerable<IReplaceStep>> Parenthesised;

        private static readonly Parser<char, IEnumerable<IReplaceStep>> TemplateParser;

        internal static IReplaceBuilder ParseTemplate(string str)
        {
            return TemplateParser
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);
        }
    }
}