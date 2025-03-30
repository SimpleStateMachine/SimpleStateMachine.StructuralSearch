namespace SimpleStateMachine.StructuralSearch;

internal readonly struct Placeholder : IPlaceholder
{
    private readonly Match<string> _match;

    public Placeholder(string name, Match<string> match)
    {
        Name = name;
        _match = match;
    }

    public string Name { get; }
    public string Value => _match.Value;
    public int Length => _match.Length;
    public LinePosition Line => _match.Line;
    public ColumnPosition Column => _match.Column;
    public OffsetPosition Offset => _match.Offset;

    public static Placeholder CreateEmpty(string name, string value)
        => new
        (
            name: name,
            match: new Match<string>
            (
                Value: value,
                Length: value.Length,
                Column: ColumnPosition.Empty,
                Line: LinePosition.Empty,
                Offset: OffsetPosition.Empty
            )
        );
}