namespace SimpleStateMachine.StructuralSearch.Rules;

public interface IRuleParameter
{
    string GetValue(ref IParsingContext context);
}