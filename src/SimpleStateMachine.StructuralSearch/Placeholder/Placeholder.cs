namespace SimpleStateMachine.StructuralSearch
{
    public class Placeholder
    {
        private IParsingContext _context;
        public Placeholder(IParsingContext context, string name, string value, FileProperty file, LineProperty line, ColumnProperty column, OffsetProperty offset)
        {
            _context = context;
            Name = name;
            Lenght = value.Length;
            Value = value;
            File = file;
            Line = line;
            Column = column;
            Offset = offset;
        }
        
        public readonly string Name;
        public readonly string Value;
        public readonly int Lenght;
        public readonly FileProperty File;
        public readonly LineProperty Line;
        public readonly ColumnProperty Column;
        public readonly OffsetProperty Offset;


        public static Placeholder CreateEmpty(IParsingContext context, string name, string value)
        {
            return new Placeholder(
                context: context,
                name: name,
                value: value,
                file: null,
                line: null,
                column: null,
                offset: null);
        }
    }
}