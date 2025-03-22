using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public interface IInput
{
    TextReader ReadData();
    string GetProperty(string propertyName);
}