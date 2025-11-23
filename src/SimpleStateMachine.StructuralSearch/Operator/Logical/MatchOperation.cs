using System.Text.RegularExpressions;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class MatchOperation(IParameter stringParameter, string regex) : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => stringParameter.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var value = stringParameter.GetValue(ref context);
        return Regex.IsMatch(input: value, pattern: regex);
    }

    public override string ToString()
        => $"{stringParameter}{Constant.Space}{Constant.Match}{Constant.Space}{Constant.DoubleQuotes}{regex}{Constant.DoubleQuotes}";
}