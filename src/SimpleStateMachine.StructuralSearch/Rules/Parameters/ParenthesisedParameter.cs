using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class ParenthesisedParameter : IRuleParameter
{
    private readonly ParenthesisType _parenthesisType;
    private readonly IRuleParameter _ruleParameter;
    private readonly string _template;

    public ParenthesisedParameter(ParenthesisType parenthesisType, IRuleParameter ruleParameter)
    {
        _parenthesisType = parenthesisType;
        _ruleParameter = ruleParameter;
        _template = GetTemplate(parenthesisType);
    }

    public string GetValue(ref IParsingContext context)
    {
        var value = _ruleParameter.GetValue(ref context);
        return string.Format(_template, value);
    }

    public override string ToString()
        => string.Format(_template, _ruleParameter);

    private static string GetTemplate(ParenthesisType parenthesisType)
        => parenthesisType switch
        {
            ParenthesisType.Usual => "({0})",
            ParenthesisType.Square => "[{0}]",
            ParenthesisType.Curly => "{{0}}",
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesisType), parenthesisType, null)
        };
}