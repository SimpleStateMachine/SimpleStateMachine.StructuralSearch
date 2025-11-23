using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parameters;
using SimpleStateMachine.StructuralSearch.Parsing;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class IsOperation(IParameter parameter, ParameterType type) : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameter.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var value = parameter.GetValue(ref context);

        return type switch
        {
            ParameterType.Var => Grammar.Identifier.Before(CommonParser.Eof).TryParse(value, out _),
            ParameterType.Int => int.TryParse(value, out _),
            ParameterType.Double => double.TryParse(value, out _),
            ParameterType.DateTime => DateTime.TryParse(value, out _),
            ParameterType.Guid => Guid.TryParse(value, out _),
            ParameterType.Bool => bool.TryParse(value, out _),
            _ => throw new ArgumentOutOfRangeException(nameof(type).FormatPrivateVar(), type, null)
        };
    }

    public override string ToString()
        => $"{parameter}{Constant.Space}{Constant.Is}{Constant.Space}{type}";
}