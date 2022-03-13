using System;
using System.Collections.Generic;
using Pidgin;
using Pidgin.Expression;
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

        public static readonly Parser<char, IRuleParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Trim()
                .Try().Select(x => new PlaceholderParameter(x))
                .As<char, PlaceholderParameter, IRuleParameter>()
                .Try();


        public static readonly Parser<char, IRuleParameter> StringParameter =
            Parser.AnyCharExcept(Constant.DoubleQuotes)
                .AtLeastOnceString()
                .Between(CommonParser.DoubleQuotes)
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .Trim()
                .Try();

        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(PlaceholderParameter, StringParameter);

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnce();

        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parsers.EnumExcept(true, Rules.SubRuleType.Is, Rules.SubRuleType.In)
                .Trim();

        public static readonly Parser<char, IRule> UnarySubRule =
            Parser.Map((type, param) => new UnarySubRule(type, param),
                    SubRuleType, Parameter)
                .As<char, UnarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parsers.Enum<PlaceholderType>(true)
                .Trim();

        public static readonly Parser<char, IRule> IsSubRule =
            Parser.Map((type, param) => new IsRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.Is), PlaceholderType)
                .As<char, IsRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> InSubRule =
            Parser.Map((type, param) => new InRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.In), Parameters)
                .As<char, InRule, IRule>()
                .Try();


        public static Parser<char, Func<IRule, IRule, IRule>> Binary(Parser<char, BinaryRuleType> op)
            => op.Select<Func<IRule, IRule, IRule>>(type => (l, r) => new BinaryRule(type, l, r));

        public static Parser<char, Func<IRule, IRule>> Unary(Parser<char, UnaryRuleType> op)
            => op.Select<Func<IRule, IRule>>(type => param => new UnaryRule(type, param));


        public static readonly Parser<char, Func<IRule, IRule, IRule>> And
            = Binary(Parsers.EnumValue(BinaryRuleType.And).Trim());

        public static readonly Parser<char, Func<IRule, IRule, IRule>> Or
            = Binary(Parsers.EnumValue(BinaryRuleType.Or).Trim());

        public static readonly Parser<char, Func<IRule, IRule, IRule>> NOR
            = Binary(Parsers.EnumValue(BinaryRuleType.NOR).Trim());

        public static readonly Parser<char, Func<IRule, IRule, IRule>> XOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XOR).Trim());

        public static readonly Parser<char, Func<IRule, IRule, IRule>> NAND
            = Binary(Parsers.EnumValue(BinaryRuleType.NAND).Trim());

        public static readonly Parser<char, Func<IRule, IRule, IRule>> XNOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XNOR).Trim());

        public static readonly Parser<char, Func<IRule, IRule>> Not
            = Unary(Parsers.EnumValue(UnaryRuleType.Not).Trim());

        public static readonly Parser<char, IRule> Expr = ExpressionParser.Build<char, IRule>(
            rule => (
                Parser.OneOf(
                    UnarySubRule,
                    IsSubRule,
                    InSubRule,
                    CommonParser.Parenthesised(rule, x => x.Trim())
                ),
                new[]
                {
                    Operator.Prefix(Not),
                    Operator.InfixL(And),
                    Operator.InfixL(NOR),
                    Operator.InfixL(XOR),
                    Operator.InfixL(NAND),
                    Operator.InfixL(XNOR),
                    Operator.InfixL(Or),
                }
            )
        );

        public static readonly Parser<char, IRule> Rule =
            Parser.Map((name, rule) => new Rule(name, rule),
                    CommonTemplateParser.Placeholder.Trim(),
                    Expr)
                .As<char, Rule, IRule>();
        
        public static IRule ParseTemplate(string str)
        {
            return Rule.ParseOrThrow(str);
        }


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

        public static readonly Parser<char, BinaryRuleType> CombinatorType =
            Parsers.Enum<BinaryRuleType>(true)
                .Trim();

        public static readonly Parser<char, BinaryRuleType> Expression =
            Parsers.Enum<BinaryRuleType>(true)
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
    //     public static readonly Parser<char, IEnumerable<IRule>> TemplateParser;
    //
    //     internal static IRulesMaster ParseTemplate(string str)
    //     {
    //         return TemplateParser
    //             .Select(steps => new RulesMaster())
    //             .ParseOrThrow(str);
    //     }
    // }
}