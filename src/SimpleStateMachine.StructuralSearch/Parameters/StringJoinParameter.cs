using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringJoinParameter : IParameter
{
    private readonly List<IParameter> _parameters;

    public StringJoinParameter(List<IParameter> parameters)
    {
        _parameters = parameters;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameters.Any(p => p.IsApplicableForPlaceholder(placeholderName));

    public string GetValue(ref IParsingContext context)
    {
        var localContext = context;
        var values = _parameters.Select(parameter => parameter.GetValue(ref localContext)).ToList();
        return string.Join(string.Empty, values);
    }

    public override string ToString()
        => string.Join(string.Empty, _parameters.Select(x => x.ToString()));
}