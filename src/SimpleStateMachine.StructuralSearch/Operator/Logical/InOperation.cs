using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class InOperation(IParameter parameter, List<IParameter> arguments) : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameter.IsApplicableForPlaceholder(placeholderName) || arguments.Any(a => a.IsApplicableForPlaceholder(placeholderName));

    public bool Execute(ref IParsingContext context)
    {
        var parameter1 = parameter.GetValue(ref context);

        foreach (var argument in arguments)
        {
            var value = argument.GetValue(ref context);

            if (Equals(parameter1, value))
                return true;
        }

        return false;
    }

    public override string ToString()
        => $"{parameter}{Constant.Space}{Constant.In}{Constant.Space}{string.Join(Constant.Comma, arguments)}";
}