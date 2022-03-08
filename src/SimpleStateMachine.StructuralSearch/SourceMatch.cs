namespace SimpleStateMachine.StructuralSearch
{
    public struct SourceMatch
    {
        public static readonly SourceMatch Empty;
        public SourceMatch(string value, int start, int end)
        {
            Value = value;
            Start = start;
            End = end;
            Lenght = value.Length;
        }
        
        public string Value { get; }
        public int Start { get; }
        public int End { get; }
        
        public int Lenght { get; }

        public override string ToString()
        {
            return Value;
        }
    }
}