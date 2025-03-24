using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class StringFormatParameter : IRuleParameter
{
    private readonly List<IRuleParameter> _parameters;

    public StringFormatParameter(List<IRuleParameter> parameters)
    {
        _parameters = parameters;
    }

    public string GetValue(ref IParsingContext context)
    {
        var localContext = context;
        return string.Join(string.Empty, _parameters.Select(parameter => parameter.GetValue(ref localContext)));
    }
        
    public override string ToString() 
        => $"{Constant.DoubleQuotes}{string.Join(string.Empty, _parameters.Select(x=> x.ToString()))}{Constant.DoubleQuotes}";
}