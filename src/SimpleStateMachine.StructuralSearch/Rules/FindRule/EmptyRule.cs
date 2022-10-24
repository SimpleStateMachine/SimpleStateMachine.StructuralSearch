namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptyRule: IRule
{
    public bool Execute()
    {
        return true;
            
    }
    public void SetContext(IParsingContext context)
    {
    }
}

public static class Rule
{
    public static readonly EmptyRule Empty = new();
}