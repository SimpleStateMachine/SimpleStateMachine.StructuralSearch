namespace SimpleStateMachine.StructuralSearch;

public readonly struct Placeholder : IPlaceholder
{
    private readonly Match<string> _match;
    private readonly IParsingContext _context;
        
    public Placeholder(ref IParsingContext context, string name, Match<string> match)
    {
        _context = context;
        Name = name;
        _match = match;
    }
        
    public string Name { get; }
    public string Value => _match.Value;
    public int Lenght => _match.Lenght;
    public LinePosition Line => _match.Line;
    public ColumnPosition Column => _match.Column;
    public OffsetPosition Offset => _match.Offset;
    public IInput Input => _context.Input;

    public static Placeholder CreateEmpty(IParsingContext context, string name, string value)
        => new
        (
            context: ref context,
            name: name,
            match: new Match<string>
            (
                Value: value, 
                Lenght: value.Length, 
                Column: ColumnPosition.Empty, 
                Line: LinePosition.Empty, 
                Offset: OffsetPosition.Empty
            )
        );
}