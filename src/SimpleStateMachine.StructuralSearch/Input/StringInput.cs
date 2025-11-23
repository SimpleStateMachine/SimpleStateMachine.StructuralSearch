using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public class StringInput(string str) : IInput
{
    public TextReader ReadData()
    {
        return new StringReader(str);
    }

    public string GetProperty(string propertyName)
    {
        var lower = propertyName.ToLower();

        return lower switch
        {
            "length" => str.Length.ToString(),
            _ => throw new ArgumentOutOfRangeException(propertyName)
        };
    }
}