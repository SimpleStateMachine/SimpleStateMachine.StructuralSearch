namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnaryOperator : IRuleOperator
    {
        public RuleOperatorType Type { get; }
        
        public string Parameter { get; }
        
        public UnaryOperator(RuleOperatorType type, string parameter)
        {
            Type = type;
            Parameter = parameter;
        }

    }
}