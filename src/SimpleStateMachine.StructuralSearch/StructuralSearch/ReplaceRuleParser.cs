using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceRuleParser
{
    private static readonly Parser<char, Assignment> Assignment =
        ParametersParser.PlaceholderParameter.Before(CommonParser.Should)
            .Then(ParametersParser.StringExpression, (placeholder, value) => new Assignment(placeholder, value));

    internal static readonly Parser<char, ReplaceRule> ReplaceRule = Parser.Map
    (
        (optionalCondition, assignments) =>
        {
            var condition = optionalCondition.HasValue ? optionalCondition.Value : new EmptyLogicalOperation();
            return new ReplaceRule(condition, assignments.ToList());
        },
        CommonParser.If.Then(LogicalExpressionParser.LogicalExpression).Before(CommonParser.Then).Optional(),
        Assignment.SeparatedAtLeastOnce(CommonParser.Comma)
    );

    // private static readonly Parser<char, Assignment> ReplaceSubRule =
    //     Parser.Map
    //     (
    //         func: (placeholder, _, parameter) => new Assignment(placeholder, parameter),
    //         parser1: ParametersParser.PlaceholderParameter.TrimStart(),
    //         parser2: CommonParser.Should.TrimStart(),
    //         parser3: ParametersParser.Parameter.TrimStart()
    //     ).Try().TrimStart();
    //
    // private static readonly Parser<char, ReplaceRule> ReplaceRule =
    //     Parser.Map
    //     (
    //         func: (rule, subRules) => new ReplaceRule(rule, subRules),
    //         parser1: ReplaceCondition.Optional().Select<IFindRule>(r => r.HasValue ? r.Value : EmptyFindRule.Instance),
    //         parser2: ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma)
    //     ).Try().TrimStart();
}