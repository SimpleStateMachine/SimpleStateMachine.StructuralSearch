using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parameters;
using SimpleStateMachine.StructuralSearch.Parsing;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class IsOperation : ILogicalOperation
{
    private readonly IParameter _parameter;
    private readonly ParameterType _type;

    public IsOperation(IParameter parameter, ParameterType type)
    {
        _parameter = parameter;
        _type = type;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var value = _parameter.GetValue(ref context);

        return _type switch
        {
            ParameterType.Var => Grammar.Identifier.Before(CommonParser.Eof).TryParse(value, out _),
            ParameterType.Int => int.TryParse(value, out _),
            ParameterType.Double => double.TryParse(value, out _),
            ParameterType.DateTime => DateTime.TryParse(value, out _),
            ParameterType.Guid => Guid.TryParse(value, out _),
            ParameterType.Bool => bool.TryParse(value, out _),
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }

    public override string ToString()
        => $"{_parameter}{Constant.Space}{Constant.Is}{Constant.Space}{_type}";
}