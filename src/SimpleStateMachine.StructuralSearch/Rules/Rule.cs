namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class Rule:IRule
    {
        public string Placeholder { get; }
        
        private IRule _rule { get; }

        public Rule(string placeholder, IRule rule)
        {
            Placeholder = placeholder;
            _rule = rule;
        }

        public bool Execute(string value)
        {
            return _rule.Execute(value);
        }
    }
}