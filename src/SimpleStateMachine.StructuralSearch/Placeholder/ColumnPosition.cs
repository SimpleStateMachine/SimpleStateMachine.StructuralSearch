namespace SimpleStateMachine.StructuralSearch
{
    public readonly record struct ColumnPosition(int Start, int End)
    {
        public readonly int Start = Start;
        public readonly int End = End;

        public static readonly ColumnPosition Empty = new(0, 0);
    }
}