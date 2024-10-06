namespace SimpleStateMachine.StructuralSearch.Rules;

internal static class IRuleParameterExtensions
{
    public static bool IsApplicableForPlaceholder(this IRuleParameter parameter, string placeholderName)
        => parameter is IPlaceholderRelatedRuleParameter relatedRuleParameter && relatedRuleParameter.Name == placeholderName;
}