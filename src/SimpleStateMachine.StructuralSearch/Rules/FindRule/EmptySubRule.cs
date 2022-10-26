namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptySubRule : IRule
{
    public bool Execute(ref IParsingContext context)
    {
        return true;
    }
    
    public override string ToString()
    {
        return $"{Constant.Underscore}";
    }

    public void SetContext(ref IParsingContext context)
    {
    }
}