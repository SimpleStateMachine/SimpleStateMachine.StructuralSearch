namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptySubRule : IRule
{
    public bool Execute()
    {
        return true;
    }
    
    public override string ToString()
    {
        return $"{Constant.Underscore}";
    }

    public void SetContext(IParsingContext context)
    {
    }
}