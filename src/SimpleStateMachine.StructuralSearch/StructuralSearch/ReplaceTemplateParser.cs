using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    internal class ReplaceTemplateParser
    {
        // static ReplaceTemplateParser()
        // {
        //     Parenthesised = Parsers.BetweenOneOfChars(x=> ParserToReplace.Stringc(x),
        //         Parser.Rec(() => Term),
        //         Constant.AllParenthesised);
        //
        //     Term = Parser.OneOf(Parenthesised, Token)
        //         .Many()
        //         .MergerMany();
        //
        //     TemplateParser = Parser.OneOf(Parenthesised, Token)
        //         .AtLeastOnceUntil(CommonParser.EOF)
        //         .MergerMany();
        // }
        //
        // internal static readonly Parser<char, IReplaceStep> Empty = 
        //     ParserToReplace.ResultAsReplace(CommonParser.Empty);
        //
        // internal static readonly Parser<char, IReplaceStep> AnyString = 
        //     ParserToReplace.ResultAsReplace(CommonParser.AnyString)
        //         .Try();
        //
        // internal static readonly Parser<char, IReplaceStep> WhiteSpaces =
        //     ParserToReplace.ResultAsReplace(CommonParser.WhiteSpaces)
        //         .Try();
        //
        // internal static readonly Parser<char, IReplaceStep> Placeholder =
        //     CommonTemplateParser.Placeholder.Select(name=> new PlaceholderReplace(name))
        //         .As<char, PlaceholderReplace, IReplaceStep>()
        //         .Try();
        //
        // internal static readonly Parser<char, IEnumerable<IReplaceStep>> Token =
        //     Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
        //         .AsMany();
        //
        // internal static readonly Parser<char, IEnumerable<IReplaceStep>> Term;
        //
        // internal static readonly Parser<char, IEnumerable<IReplaceStep>> Parenthesised;
        //
        // internal static readonly Parser<char, IEnumerable<IReplaceStep>> TemplateParser;
        //
        // internal static IReplaceBuilder ParseTemplate(string str)
        // {
        //     return TemplateParser
        //         .Select(steps => new ReplaceBuilder(steps))
        //         .ParseOrThrow(str);
        // }
        
        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(ParametersParser.StringParameter,
                    ParametersParser.PlaceholderOrPropertyRuleParameter)
                .Then(ParametersParser.ChangeParameter, (parameter, func) => func(parameter))
                .Try();

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnceUntil(CommonParser.EOF)
                .Try();
        
        internal static IReplaceBuilder ParseTemplate(string str)
        {
            return Parameters
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);
        }
    }
}