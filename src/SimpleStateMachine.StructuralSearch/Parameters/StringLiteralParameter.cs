using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringLiteralParameter : IParameter
{
    private readonly string _value;

    public StringLiteralParameter(string value)
    {
        _value = value;
    }

    public bool IsApplicableForPlaceholder(string placeholderName) => false;

    public string GetValue(ref IParsingContext context) 
        => _value;

    public override string ToString()
        => $"{Constant.DoubleQuotes}{EscapeHelper.Escape(_value, Constant.StringLiteralCharsToEscape)}{Constant.DoubleQuotes}";
}