namespace SimpleStateMachine.StructuralSearch;

internal interface IContextDependent
{
    void SetContext(ref IParsingContext context);
}