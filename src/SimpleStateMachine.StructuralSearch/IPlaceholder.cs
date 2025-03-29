namespace SimpleStateMachine.StructuralSearch;

public interface IPlaceholder
{
    string Name { get; }
    string Value { get; }
    int Length { get; }
    LinePosition Line { get; }
    ColumnPosition Column { get; }
    OffsetPosition Offset { get; }
}