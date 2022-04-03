namespace SimpleStateMachine.StructuralSearch
{
    public interface IContextDependent
    {
        void SetContext(IParsingContext context);
    }
}