using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules;

internal class StringFormatParameter : IRuleParameter
{
    private readonly IEnumerable<IRuleParameter> _parameters;

    public StringFormatParameter(IEnumerable<IRuleParameter> parameters)
    {
        _parameters = parameters;
    }

    public string GetValue(ref IParsingContext context)
    {
        var values = new List<string>();
        foreach (var parameter in _parameters)
        {
            values.Add(parameter.GetValue(ref context));
        }
        
        return string.Join(string.Empty, values);
    }
        
    public override string ToString() 
        => $"{Constant.DoubleQuotes}{string.Join(string.Empty, _parameters.Select(x=> x.ToString()))}{Constant.DoubleQuotes}";
}