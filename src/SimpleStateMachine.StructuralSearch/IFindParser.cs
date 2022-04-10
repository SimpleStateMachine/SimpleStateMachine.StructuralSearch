namespace SimpleStateMachine.StructuralSearch
{
    public interface IFindParser
    {
        SourceMatch Parse(IParsingContext context, string input);
    }
}