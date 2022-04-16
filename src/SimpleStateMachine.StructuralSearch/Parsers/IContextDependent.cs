namespace SimpleStateMachine.StructuralSearch
{
    public interface IContextDependent
    {
        void SetContext(ref IParsingContext context);
    }
}