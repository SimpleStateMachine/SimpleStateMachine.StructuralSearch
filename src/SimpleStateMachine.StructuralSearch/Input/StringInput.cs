using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public class StringInput : IInput
{
    private readonly string _str;

    public StringInput(string str)
    {
        _str = str;
    }

    public TextReader ReadData()
        => new StringReader(_str);

    public string GetProperty(string propertyName)
    {
        var lower = propertyName.ToLower();

        return lower switch
        {
            "length" => _str.Length.ToString(),
            _ => throw new ArgumentOutOfRangeException(propertyName)
        };
    }
}