using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class StringJoinParameter : IRuleParameter
{
    private readonly List<IRuleParameter> _parameters;

    public StringJoinParameter(List<IRuleParameter> parameters)
    {
        _parameters = parameters;
    }

    public string GetValue(ref IParsingContext context)
    {
        var localContext = context;
        var values = _parameters.Select(parameter => parameter.GetValue(ref localContext)).ToList();
        return string.Join(string.Empty, values);
    }

    public override string ToString()
        => string.Join(string.Empty, _parameters.Select(x => x.ToString()));
}