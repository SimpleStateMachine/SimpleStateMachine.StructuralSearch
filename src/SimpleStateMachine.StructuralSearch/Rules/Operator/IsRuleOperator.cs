namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class IsRuleOperator : IRuleOperator
    {
        public RuleOperatorType Type { get; }
        
        public PlaceholderType PlaceholderType { get; }
        
        public IsRuleOperator(RuleOperatorType type, PlaceholderType placeholderType)
        {
            Type = type;
            PlaceholderType = placeholderType;
        }
    }
}