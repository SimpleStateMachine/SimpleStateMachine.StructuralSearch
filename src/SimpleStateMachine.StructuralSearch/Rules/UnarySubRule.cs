namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnarySubRule : IRule
    {
        public SubRuleType Type { get; }
        
        public IRuleParameter Parameter { get; }
        
        public UnarySubRule(SubRuleType type, IRuleParameter parameter)
        {
            Type = type;
            Parameter = parameter;
        }

        public bool Execute(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}