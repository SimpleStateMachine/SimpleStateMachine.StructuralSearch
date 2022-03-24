namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class FindRule
    {
        public string Placeholder { get; }
        
        private IRule _rule { get; }

        public FindRule(string placeholder, IRule rule)
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