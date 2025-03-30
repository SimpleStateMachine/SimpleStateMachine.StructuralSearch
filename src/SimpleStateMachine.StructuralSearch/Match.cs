namespace SimpleStateMachine.StructuralSearch;

public readonly record struct Match<T>(T Value, int Length, ColumnPosition Column, LinePosition Line, OffsetPosition Offset);