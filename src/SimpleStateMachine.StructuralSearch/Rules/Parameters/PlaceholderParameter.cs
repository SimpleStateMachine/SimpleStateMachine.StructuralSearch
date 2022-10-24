namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderParameter : IRuleParameter
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
        
        public IPlaceholder GetPlaceholder()
        {
            return _context.GetPlaceholder(Name);
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }

        public void SetContext(IParsingContext context)
        {
            _context = context;
        }
    }
}