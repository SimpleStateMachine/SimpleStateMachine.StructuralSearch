namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate;

public interface IReplaceStep
{
    string GetValue(ref IParsingContext context);
}