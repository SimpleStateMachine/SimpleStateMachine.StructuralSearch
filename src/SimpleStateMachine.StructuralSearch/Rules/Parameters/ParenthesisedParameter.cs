using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

public class ParenthesisedParameter : IRuleParameter
{
    private readonly IEnumerable<IRuleParameter> _parameters;
    private readonly ParenthesisType _parenthesisType;
    private readonly string _template;
    public ParenthesisedParameter(ParenthesisType parenthesisType, IEnumerable<IRuleParameter> parameters)
    {
        _parenthesisType = parenthesisType;
        _parameters = parameters;
        _template = GetTemplate(parenthesisType);
    }

    public string GetValue()
    {
        return string.Format(_template, string.Join(string.Empty, _parameters.Select(x => x.GetValue())));
    }
        
    public override string ToString()
    {
        return string.Format(_template, string.Join(string.Empty, _parameters.Select(x=> x.ToString())));
    }

    public void SetContext(ref IParsingContext context)
    {
        foreach (var parameter in _parameters)
        {
            parameter.SetContext(ref context);
        }
    }

    private static string GetTemplate(ParenthesisType parenthesisType)
    {
        return parenthesisType switch
        {
            ParenthesisType.Usual => "({0})",
            ParenthesisType.Square => "[{0}]",
            ParenthesisType.Curly => "{{0}}",
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesisType), parenthesisType, null)
        };
    }
}