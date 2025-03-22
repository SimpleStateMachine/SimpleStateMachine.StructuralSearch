namespace SimpleStateMachine.StructuralSearch;

public readonly record struct LinePosition(int Start, int End)
{
    public readonly int Start = Start;
    public readonly int End = End;
        
    public static readonly LinePosition Empty = new(0, 0);
}