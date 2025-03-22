namespace SimpleStateMachine.StructuralSearch;

public readonly record struct Match<T>(T Value, int Lenght, ColumnPosition Column, LinePosition Line, OffsetPosition Offset);