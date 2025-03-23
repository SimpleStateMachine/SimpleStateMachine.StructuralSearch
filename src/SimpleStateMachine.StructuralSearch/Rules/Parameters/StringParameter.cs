using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class StringParameter : IRuleParameter
{
    private readonly string _value;
        
    public StringParameter(string value)
    {
        _value = value;
    }
    public string GetValue(ref IParsingContext context) 
        => _value;

    public override string ToString()
    {
        var value = EscapeHelper.EscapeChars(_value, c => $"{Constant.BackSlash}{c}", Constant.CharsToEscape);
        return $"{value}";
    }
}