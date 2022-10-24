namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class PlaceholderReplace : IReplaceStep, IContextDependent
    {
        private IParsingContext _context;
        private readonly string _name;

        public PlaceholderReplace(string name)
        {
            _name = name;
        }

        public string GetValue()
        {
            return _context.GetPlaceholder(_name).Value;
        }

        public void SetContext(ref IParsingContext context)
        {
            _context = context;
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{_name}{Constant.PlaceholderSeparator}";
        }  
    }
}