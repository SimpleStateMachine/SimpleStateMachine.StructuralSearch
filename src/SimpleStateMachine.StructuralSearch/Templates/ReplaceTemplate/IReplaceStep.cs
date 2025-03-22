namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate;

internal interface IReplaceStep
{
    string GetValue(ref IParsingContext context);
}