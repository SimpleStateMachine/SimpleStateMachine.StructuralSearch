using SimpleStateMachine.StructuralSearch.Parsers;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters
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
        
        public Placeholder.Placeholder GetPlaceholder()
        {
            return _context.GetPlaceholder(Name);
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }

        public void SetContext(IParsingContext? context)
        {
            _context = context;
        }
    }
}