namespace SimpleStateMachine.StructuralSearch.Placeholder;

internal readonly struct PlaceholderParser(string name, Match<string> match) : IPlaceholder
{
    public string Name { get; } = name;
    public string Value => match.Value;
    public int Length => match.Length;
    public LinePosition Line => match.Line;
    public ColumnPosition Column => match.Column;
    public OffsetPosition Offset => match.Offset;

    public static PlaceholderParser CreateEmpty(string name, string value)
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