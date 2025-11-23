using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringJoinParameter(List<IParameter> parameters) : IParameter
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameters.Any(p => p.IsApplicableForPlaceholder(placeholderName));

    public string GetValue(ref IParsingContext context)
    {
        var localContext = context;
        var values = parameters.Select(parameter => parameter.GetValue(ref localContext)).ToList();
        return string.Join(string.Empty, values);
    }

    public override string ToString()
        => string.Join(string.Empty, parameters.Select(x => x.ToString()));
}