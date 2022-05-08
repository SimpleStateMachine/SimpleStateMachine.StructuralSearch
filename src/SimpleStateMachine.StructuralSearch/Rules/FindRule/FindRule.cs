namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class FindRule
    {
        public PlaceholderParameter Placeholder { get; }
        
        private IRule _rule { get; }

        public FindRule(PlaceholderParameter placeholder, IRule rule)
        {
            Placeholder = placeholder;
            _rule = rule;
        }
        
        public override string ToString()
        {
            return $"{Placeholder}{Constant.Space}{_rule}";
        }

        public bool Execute()
        {
            var value = Placeholder.GetValue();
            return _rule.Execute(value);
        }
    }
}