namespace SimpleStateMachine.StructuralSearch;

internal class PlaceholderReplace : IPlaceholder
{
    private readonly IPlaceholder _placeholder;
    private readonly string _newValue;

    public PlaceholderReplace(IPlaceholder placeholder, string newValue)
    {
        _placeholder = placeholder;
        _newValue = newValue;
    }

    public string Name => _placeholder.Name;
    public string Value => _newValue;
    public int Length => _newValue.Length;
    public LinePosition Line => _placeholder.Line;
    public ColumnPosition Column => _placeholder.Column;
    public OffsetPosition Offset => _placeholder.Offset;
}