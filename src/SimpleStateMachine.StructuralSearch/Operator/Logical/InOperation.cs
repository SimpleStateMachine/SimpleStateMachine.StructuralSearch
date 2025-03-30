using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class InOperation : ILogicalOperation
{
    private readonly IParameter _parameter;
    private readonly List<IParameter> _arguments;

    public InOperation(IParameter parameter, List<IParameter> arguments)
    {
        _parameter = parameter;
        _arguments = arguments;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName) || _arguments.Any(a => a.IsApplicableForPlaceholder(placeholderName));

    public bool Execute(ref IParsingContext context)
    {
        var parameter = _parameter.GetValue(ref context);

        foreach (var argument in _arguments)
        {
            var value = argument.GetValue(ref context);

            if (Equals(parameter, value))
                return true;
        }

        return false;
    }

    public override string ToString()
        => $"{_parameter}{Constant.Space}{Constant.In}{Constant.Space}{string.Join(Constant.Comma, _arguments)}";
}