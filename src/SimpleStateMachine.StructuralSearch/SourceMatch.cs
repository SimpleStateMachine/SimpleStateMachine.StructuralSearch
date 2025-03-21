namespace SimpleStateMachine.StructuralSearch;

public readonly struct SourceMatch
{
    public readonly string Value;
    public readonly int Start;
    public readonly int End;
    public readonly int Lenght;
        
    public SourceMatch(string value, int start, int end)
    {
        Value = value;
        Start = start;
        End = end;
        Lenght = value.Length;
    }

    public override string ToString()
    {
        var value =  Value switch
        {
            "\r\n" => "\\r\\n",
            "\n" => "\\n",
            _ => Value
        };

        return $"'{value}'";
    }
}