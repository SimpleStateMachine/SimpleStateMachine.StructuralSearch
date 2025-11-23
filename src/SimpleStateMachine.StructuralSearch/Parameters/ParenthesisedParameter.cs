using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class ParenthesisedParameter(ParenthesisType parenthesisType, IParameter parameter) : IParameter
{
    private readonly string _template = GetTemplate(parenthesisType);

    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameter.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var value = parameter.GetValue(ref context);
        return string.Format(_template, value);
    }

    public override string ToString()
        => string.Format(_template, parameter);

    private static string GetTemplate(ParenthesisType parenthesisType)
        => parenthesisType switch
        {
            ParenthesisType.Usual => "({0})",
            ParenthesisType.Square => "[{0}]",
            ParenthesisType.Curly => "{{0}}",
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesisType), parenthesisType, null)
        };
}