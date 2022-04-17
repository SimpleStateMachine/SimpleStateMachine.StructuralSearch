namespace SimpleStateMachine.StructuralSearch
{
    public class Placeholder
    {
        private IParsingContext _context;
        public Placeholder(IParsingContext context, string name, string value, LineProperty line, ColumnProperty column, OffsetProperty offset)
        {
            _context = context;
            Name = name;
            Lenght = value.Length;
            Value = value;
            Line = line;
            Column = column;
            Offset = offset;
        }
        
        public readonly string Name;
        public readonly string Value;
        public readonly int Lenght;
        public readonly LineProperty Line;
        public readonly ColumnProperty Column;
        public readonly OffsetProperty Offset;
        public IInput Input => _context.Input;

        public static Placeholder CreateEmpty(IParsingContext context, string name, string value)
        {
            return new Placeholder(
                context: context,
                name: name,
                value: value,
                line: null,
                column: null,
                offset: null);
        }
    }
}