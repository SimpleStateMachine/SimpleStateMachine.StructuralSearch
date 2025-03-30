using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class ParenthesisedParameter : IParameter
{
    private readonly ParenthesisType _parenthesisType;
    private readonly IParameter _parameter;
    private readonly string _template;

    public ParenthesisedParameter(ParenthesisType parenthesisType, IParameter parameter)
    {
        _parenthesisType = parenthesisType;
        _parameter = parameter;
        _template = GetTemplate(parenthesisType);
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var value = _parameter.GetValue(ref context);
        return string.Format(_template, value);
    }

    public override string ToString()
        => string.Format(_template, _parameter);

    private static string GetTemplate(ParenthesisType parenthesisType)
        => parenthesisType switch
        {
            ParenthesisType.Usual => "({0})",
            ParenthesisType.Square => "[{0}]",
            ParenthesisType.Curly => "{{0}}",
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesisType), parenthesisType, null)
        };
}