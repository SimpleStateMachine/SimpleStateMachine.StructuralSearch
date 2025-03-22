namespace SimpleStateMachine.StructuralSearch.Rules;

internal interface IRuleParameter
{
    string GetValue(ref IParsingContext context);
}