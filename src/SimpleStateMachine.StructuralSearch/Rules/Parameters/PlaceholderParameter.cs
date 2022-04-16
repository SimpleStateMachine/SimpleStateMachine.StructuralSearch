namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderParameter : IRuleParameter, IContextDependent
    {
        private IParsingContext _context;
        
        public PlaceholderParameter(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        
        public string GetValue()
        {
            return _context.GetPlaceholder(Name).Value;
        }
        
        public Placeholder GetPlaceholder()
        {
            return _context.GetPlaceholder(Name);
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _context = context;
        }
    }
}