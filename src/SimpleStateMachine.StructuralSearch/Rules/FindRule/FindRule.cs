namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class FindRule
    {
        public IRuleParameter Placeholder { get; }
        
        private IRule _rule { get; }

        public FindRule(IRuleParameter placeholder, IRule rule)
        {
            Placeholder = placeholder;
            _rule = rule;
        }
        
        public override string ToString()
        {
            return $"{Placeholder}{Constant.Space}{_rule}";
        } 
    }
}