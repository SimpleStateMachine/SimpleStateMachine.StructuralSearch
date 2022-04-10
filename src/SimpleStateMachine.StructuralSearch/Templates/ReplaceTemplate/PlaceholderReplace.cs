using SimpleStateMachine.StructuralSearch.Parsers;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate
{
    public class PlaceholderReplace : IReplaceStep, IContextDependent
    {
        private IParsingContext _context;
        
        public string Name { get; }

        public PlaceholderReplace(string name)
        {
            Name = name;
        }

        public string GetValue()
        {
            return _context.GetPlaceholder(Name).Value;
        }

        public void SetContext(IParsingContext context)
        {
            _context = context;
        }
    }
}