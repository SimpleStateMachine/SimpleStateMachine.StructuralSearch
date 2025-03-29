using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringParameter : IParameter
{
    private readonly string _value;
        
    public StringParameter(string value)
    {
        _value = value;
    }
    public string GetValue(ref IParsingContext context) 
        => _value;

    public override string ToString()
        => $"{Constant.DoubleQuotes}{EscapeHelper.Escape(_value, Constant.StringLiteralCharsToEscape)}{Constant.DoubleQuotes}";
}