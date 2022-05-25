namespace SimpleStateMachine.StructuralSearch.Rules
{
    public interface IRuleParameter : IContextDependent
    {
        string GetValue();
    }
}