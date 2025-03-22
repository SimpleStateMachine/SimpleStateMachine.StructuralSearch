namespace SimpleStateMachine.StructuralSearch;

public readonly record struct OffsetPosition(int Start, int End)
{
    public readonly int Start = Start;
    public readonly int End = End;

    public static readonly OffsetPosition Empty = new(0, 0);
}