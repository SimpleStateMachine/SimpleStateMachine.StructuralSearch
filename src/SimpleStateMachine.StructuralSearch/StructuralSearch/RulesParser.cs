using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class FindRuleParser
    {
        static FindRuleParser()
        {
            // Parenthesised = Parsers.BetweenOneOf(x => ParserToReplace.Stringc(x),
            //     Parser.Rec(() => Term),
            //     Constant.AllParenthesised);
            //
            // Term = Parser.OneOf(Parenthesised, Token)
            //     .Many()
            //     .MergerMany();
            //
            // TemplateParser = Parser.OneOf(Parenthesised, Token)
            //     .AtLeastOnce()
            //     .MergerMany();
        }
        
        public static readonly Parser<char, string> Placeholder =
            CommonTemplateParser.Placeholder
                .Trim()
                .Try();

        public static readonly Parser<char, IEnumerable<string>> Placeholders =
            Placeholder.Separated(CommonParser.Comma);

        public static readonly Parser<char, string> Value =
            Parser.AnyCharExcept(Constant.DoubleQuotes)
                .AtLeastOnceString()
                .Between(CommonParser.DoubleQuotes)
                .Trim();
        
        public static readonly Parser<char, IEnumerable<string>> Values =
            CommonParser.Parenthesised(Value.Separated(CommonParser.Comma), 
                    x=> x.Trim())
                .Trim();

        public static readonly Parser<char, RuleOperatorType> UnaryOperatorType =
            Parsers.EnumExcept(true, RuleOperatorType.Is, RuleOperatorType.In)
                .Trim();
        
        public static readonly Parser<char, IRuleOperator> UnaryOperator =
            Parser.Map((type, param) => new UnaryOperator(type, param),
                UnaryOperatorType, Value)
                .As<char, UnaryOperator, IRuleOperator>()
                .Try();
            
        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parsers.Enum<PlaceholderType>(true)
                .Trim();
        
        public static readonly Parser<char, IRuleOperator> IsOperator =
                Parser.Map((type, param) => new IsRuleOperator(type, param),
                        Parsers.EnumValue(RuleOperatorType.Is), PlaceholderType)
                    .As<char, IsRuleOperator, IRuleOperator>()
                    .Try();
        
        public static readonly Parser<char, IRuleOperator> InOperator =
            Parser.Map((type, param) => new InRuleOperator(type, param),
                    Parsers.EnumValue(RuleOperatorType.In), Values)
                .As<char, InRuleOperator, IRuleOperator>()
                .Try();

        public static readonly Parser<char, IRuleOperator> Operator =
            Parser.OneOf(UnaryOperator, IsOperator, InOperator);
      
        
        
        // public static readonly Parser<char, IRule> Rule =
        //     Parser.Map();
        
        
        // public static readonly Parser<char, RuleOperatorType> OperatorType =
        //     Parsers.Enum<RuleOperatorType>(true)
        //         .Trim();
        //
        // public static readonly Parser<char, IRuleOperator> Operator =
        //     Parser.Map((isInverse, type) => new RuleOperator(type, isInverse),
        //             IsInverse, OperatorType)
        //         .As<char, RuleOperator, IRuleOperator>();

        public static readonly Parser<char, BinarySubRuleType> CombinatorType =
            Parsers.Enum<BinarySubRuleType>(true)
                .Trim();
        
        public static readonly Parser<char, BinarySubRuleType> Expression =
            Parsers.Enum<BinarySubRuleType>(true)
                .Trim();
        public static readonly Parser<char, IEnumerable<IRule>> Parenthesised;
        
        // public static readonly Parser<char, IRule> Rule =
        //     Parser.Map(_, Placeholder, Operator);
        //     Placeholder.Then();
        
        // public static readonly Parser<char, RuleCombinatorType> Combinator =
        //     Parser.Map(_, Operator, CombinatorType, Operator);
        // public static readonly Parser<char, IEnumerable<IRuleOperator>> Operators =
        //     Operator.Separated(CombinatorType);
    }
    
    // public static class RulesParser
    // {
    //     static RulesParser()
    //     {
    //         Parenthesised = Parsers.BetweenOneOfChars(ParserToReplace.Stringc,
    //             Parser.Rec(() => Term),
    //             Constant.AllParenthesised);
    //
    //         Term = Parser.OneOf(Parenthesised, Token)
    //             .Many()
    //             .MergerMany();
    //
    //         TemplateParser = Parser.OneOf(Parenthesised, Token)
    //             .AtLeastOnce()
    //             .MergerMany();
    //     }
    //
    //     public static readonly Parser<char, IRule> Empty = 
    //         ParserToReplace.ResultAsReplace(CommonParser.Empty);
    //
    //     public static readonly Parser<char, IRule> AnyString = 
    //         ParserToReplace.ResultAsReplace(CommonParser.AnyString)
    //             .Try();
    //
    //     public static readonly Parser<char, IRule> WhiteSpaces =
    //         ParserToReplace.ResultAsReplace(CommonParser.WhiteSpaces)
    //             .Try();
    //
    //     public static readonly Parser<char, IRule> Placeholder =
    //         CommonTemplateParser.Placeholder.Select(name=> new PlaceholderReplace(name))
    //             .As<char, PlaceholderReplace, IRule>()
    //             .Try();
    //
    //     public static readonly Parser<char, IEnumerable<IRule>> Token =
    //         Parser.OneOf(AnyString, Placeholder, WhiteSpaces)
    //             .AsMany();
    //
    //     public static readonly Parser<char, IEnumerable<IRule>> Term;
    //
    //     public static readonly Parser<char, IEnumerable<IRule>> Parenthesised;
    //
    //     private static readonly Parser<char, IEnumerable<IRule>> TemplateParser;
    //
    //     internal static IRulesMaster ParseTemplate(string str)
    //     {
    //         return TemplateParser
    //             .Select(steps => new RulesMaster())
    //             .ParseOrThrow(str);
    //     }
    // }
}