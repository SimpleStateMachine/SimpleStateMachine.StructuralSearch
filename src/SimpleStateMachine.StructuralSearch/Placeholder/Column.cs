namespace SimpleStateMachine.StructuralSearch
{
    public class ColumnProperty
    {
        public ColumnProperty(int start, int end)
        {
            Start = start;
            End = end;
        }
        
        public readonly int Start;
        public readonly int End;
    }
}