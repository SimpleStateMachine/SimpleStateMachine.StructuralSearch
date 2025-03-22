namespace SimpleStateMachine.StructuralSearch.Placeholder;

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
    public int Lenght => _match.Lenght;
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
                Lenght: value.Length,
                Column: ColumnPosition.Empty,
                Line: LinePosition.Empty,
                Offset: OffsetPosition.Empty
            )
        );
}