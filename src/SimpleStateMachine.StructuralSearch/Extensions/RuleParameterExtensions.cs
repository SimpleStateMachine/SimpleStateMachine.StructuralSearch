using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class RuleParameterExtensions
{
    public static bool IsApplicableForPlaceholder(this IRuleParameter parameter, string placeholderName)
        => parameter is IPlaceholderRelatedRuleParameter relatedRuleParameter && relatedRuleParameter.PlaceholderName == placeholderName;
}