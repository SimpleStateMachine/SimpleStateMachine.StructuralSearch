using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public class EmptyInput : IInput
{
    public TextReader ReadData()
        => new StringReader(string.Empty);

    public string GetProperty(string propertyName) => throw new NotImplementedException();
}