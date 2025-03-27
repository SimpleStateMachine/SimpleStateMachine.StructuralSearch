using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindSubRuleParser
{
    private static readonly Parser<char, SubRuleType> SubRuleType = Parser.CIEnum<SubRuleType>().Trim();
    private static readonly Parser<char, PlaceholderType> PlaceholderType = Parser.CIEnum<PlaceholderType>().TrimStart();

    private static Parser<char, IFindRule> BinarySubRule(IRuleParameter left, SubRuleType ruleType) =>
        ParametersParser.Parameter
            .TrimStart()
            .Select<IFindRule>(right => new BinarySubRule(ruleType, left, right))
            .Try();

    private static Parser<char, IFindRule> IsSubRule(IRuleParameter left) =>
        PlaceholderType.Select<IFindRule>(arg => new IsSubRule(left, arg))
            .Try();

    private static Parser<char, IFindRule> InSubRule(IRuleParameter left) =>
        ParametersParser.Parameters
            .ParenthesisedOptional(x => Parser.Char(x).Trim())
            .TrimStart()
            .Select<IFindRule>(args => new InSubRule(left, args))
            .Try();

    internal static readonly Parser<char, IFindRule> OneOfSubRule =
        Parser.Map
            (
                func: (left, ruleType) => (value: left, type: ruleType),
                parser1: Parser.OneOf
                (
                    PlaceholderPropertyParser.PlaceholderPropertyParameter.Cast<IRuleParameter>().Try(), 
                    ParametersParser.PlaceholderParameter.Cast<IRuleParameter>()
                ).Trim(),
                parser2: SubRuleType
            )
            .Then(x => x.type switch
            {
                Rules.FindRules.Types.SubRuleType.In => InSubRule(x.value),
                Rules.FindRules.Types.SubRuleType.Is => IsSubRule(x.value),
                _ => BinarySubRule(x.value, x.type)
            });
}