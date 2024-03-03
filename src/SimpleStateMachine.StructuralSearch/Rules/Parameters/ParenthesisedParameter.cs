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

    public string GetValue(ref IParsingContext context)
    {
        var values = new List<string>();
        foreach (var parameter in _parameters)
        {
            values.Add(parameter.GetValue(ref context));
        }
        
        return string.Format(_template, string.Join(string.Empty, values));
    }
        
    public override string ToString() 
        => string.Format(_template, string.Join(string.Empty, _parameters.Select(x=> x.ToString())));

    private static string GetTemplate(ParenthesisType parenthesisType) 
        => parenthesisType switch
        {
            ParenthesisType.Usual => "({0})",
            ParenthesisType.Square => "[{0}]",
            ParenthesisType.Curly => "{{0}}",
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesisType), parenthesisType, null)
        };
}