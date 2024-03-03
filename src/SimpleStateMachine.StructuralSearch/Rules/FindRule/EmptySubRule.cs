namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptySubRule : IRule
{
    public bool Execute(ref IParsingContext context) 
        => true;

    public override string ToString() 
        => $"{Constant.Underscore}";
}