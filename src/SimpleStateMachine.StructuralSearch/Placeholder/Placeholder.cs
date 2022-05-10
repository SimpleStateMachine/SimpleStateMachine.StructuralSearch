namespace SimpleStateMachine.StructuralSearch
{
    public readonly struct Placeholder
    {
        private readonly Match<string> _match;
        private readonly IParsingContext _context;
        public Placeholder(IParsingContext context, string name, Match<string> match)
        {
            _context = context;
            Name = name;
            _match = match;
        }
        
        public readonly string Name;
        public string Value => _match.Value;
        public int Lenght => _match.Lenght;
        public LinePosition Line => _match.Line;
        public ColumnPosition Column => _match.Column;
        public OffsetPosition Offset => _match.Offset;
        public IInput Input => _context.Input;

        public static Placeholder CreateEmpty(IParsingContext context, string name, string value)
        {
            return new Placeholder(
                context: context,
                name: name,
                new Match<string>(
                    value, 
                    value.Length, 
                    ColumnPosition.Empty, 
                    LinePosition.Empty, 
                    OffsetPosition.Empty));
        }
    }
}