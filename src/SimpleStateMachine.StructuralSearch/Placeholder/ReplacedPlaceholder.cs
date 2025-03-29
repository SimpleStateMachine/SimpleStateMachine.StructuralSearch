namespace SimpleStateMachine.StructuralSearch.Placeholder;

internal class ReplacedPlaceholder : IPlaceholder
{
    public ReplacedPlaceholder(IPlaceholder placeholder, string value)
    {
        Name = placeholder.Name;
        Value = value;
        Length = value.Length;
        Line = placeholder.Line;
        Column = placeholder.Column;
        Offset = placeholder.Offset;
    }
    
    public string Name { get; }
    public string Value { get; }
    public int Length { get; }
    public LinePosition Line { get; }
    public ColumnPosition Column { get; }
    public OffsetPosition Offset { get; }
}