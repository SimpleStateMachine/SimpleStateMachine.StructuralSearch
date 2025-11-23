namespace SimpleStateMachine.StructuralSearch.Placeholder;

internal class PlaceholderReplace(IPlaceholder placeholder, string newValue) : IPlaceholder
{
    public string Name => placeholder.Name;
    public string Value => newValue;
    public int Length => newValue.Length;
    public LinePosition Line => placeholder.Line;
    public ColumnPosition Column => placeholder.Column;
    public OffsetPosition Offset => placeholder.Offset;
}