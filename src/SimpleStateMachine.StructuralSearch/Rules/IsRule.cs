namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class IsRule : IRule
    {
        public SubRuleType Type { get; }
        
        public PlaceholderType PlaceholderType { get; }
        
        public IsRule(SubRuleType type, PlaceholderType placeholderType)
        {
            Type = type;
            PlaceholderType = placeholderType;
        }

        public bool Execute(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}