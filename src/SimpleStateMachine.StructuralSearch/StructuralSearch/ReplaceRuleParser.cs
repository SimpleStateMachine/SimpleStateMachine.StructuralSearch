using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceRuleParser
{
    internal static readonly Parser<char, Assignment> Assignment =
        ParametersParser.PlaceholderParameter.TrimEnd().Before(CommonParser.Should).TrimEnd()
            .Then(ParametersParser.StringExpression, (placeholder, value) => new Assignment(placeholder, value));

    internal static readonly Parser<char, IReplaceCondition> ReplaceRuleCondition =
        CommonParser.If.TrimEnd().Then(LogicalExpressionParser.LogicalExpression)
            .Before(CommonParser.Then.TrimEnd()).Optional()
            .Select<IReplaceCondition>(operation =>
            {
                var op = operation.HasValue ? operation.Value : null;
                return new ReplaceCondition(op);
            });
    
    internal static readonly Parser<char, ReplaceRule> ReplaceRule = Parser.Map
    (
        (condition, assignments) => new ReplaceRule(condition, assignments.ToList()),
        ReplaceRuleCondition,
        Assignment.SeparatedAtLeastOnce(CommonParser.Comma.TrimEnd())
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