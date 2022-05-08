namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderLogicalRule : ILogicalRule
    {
        public PlaceholderParameter Placeholder { get; }
        
        private IRule _rule { get; }

        public PlaceholderLogicalRule(PlaceholderParameter placeholder, IRule rule)
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