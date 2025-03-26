namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal interface IPlaceholderPropertyRuleParameter : IRuleParameter
{
    PlaceholderParameter Placeholder { get; }
}