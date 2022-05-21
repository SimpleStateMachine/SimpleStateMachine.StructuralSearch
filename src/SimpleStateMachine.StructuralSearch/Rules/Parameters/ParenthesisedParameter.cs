using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

public class ParenthesisedParameter : IRuleParameter
{
    public readonly IEnumerable<IRuleParameter> Parameters;
    public readonly ParenthesisType ParenthesisType;
    public readonly string Template;
    public ParenthesisedParameter(ParenthesisType parenthesisType, IEnumerable<IRuleParameter> parameters)
    {
        ParenthesisType = parenthesisType;
        Parameters = parameters;
        Template = GetTemplate(parenthesisType);
    }

    public string GetValue()
    {
        return string.Format(Template, string.Join(string.Empty, Parameters.Select(x => x.GetValue())));
    }
        
    public override string ToString()
    {
        return string.Format(Template, string.Join(string.Empty, Parameters.Select(x=> x.ToString())));
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