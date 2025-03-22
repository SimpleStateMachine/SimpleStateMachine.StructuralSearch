namespace SimpleStateMachine.StructuralSearch.Rules;

internal interface IPlaceholderRelatedRuleParameter : IRuleParameter
{
    string PlaceholderName { get; }
}