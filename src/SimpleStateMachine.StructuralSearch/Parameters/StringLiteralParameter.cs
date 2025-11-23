using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringLiteralParameter(string value) : IParameter
{
    public bool IsApplicableForPlaceholder(string placeholderName) => false;

    public string GetValue(ref IParsingContext context) 
        => value;

    public override string ToString()
        => $"{Constant.DoubleQuotes}{EscapeHelper.Escape(value, Constant.StringLiteralCharsToEscape)}{Constant.DoubleQuotes}";
}