namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderParameter : IRuleParameter, IContextDependent
    {
        private ParsingContext _context;
        
        public PlaceholderParameter(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        
        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }

        public void SetContext(ParsingContext context)
        {
            _context = context;
        }
    }
}