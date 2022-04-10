namespace SimpleStateMachine.StructuralSearch.Placeholder
{
    public class Placeholder
    {
        private IParsingContext _context;
        public Placeholder(IParsingContext context, string name, string value)
        {
            _context = context;
            Name = name;
            Value = value;
        }
        
        public readonly string Name;
        public readonly string Value;
        public readonly int Lenght;
        public readonly FileProperty File;
        public readonly LineProperty Line;
        public readonly Column Column;
        public readonly OffsetProperty Offset;
    }
}