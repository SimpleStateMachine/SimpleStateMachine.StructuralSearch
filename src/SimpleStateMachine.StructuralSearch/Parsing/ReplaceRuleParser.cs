using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Replace;

namespace SimpleStateMachine.StructuralSearch.Parsing;

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
}