namespace SimpleStateMachine.StructuralSearch.Parsers
{
    public interface IContextDependent
    {
        void SetContext(IParsingContext context);
    }
}