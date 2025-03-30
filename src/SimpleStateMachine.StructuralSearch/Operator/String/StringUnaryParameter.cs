using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.String;

internal class StringUnaryParameter : IParameter
{
    private readonly IParameter _parameter;
    private readonly StringUnaryOperator _type;

    public StringUnaryParameter(IParameter parameter, StringUnaryOperator type)
    {
        _parameter = parameter;
        _type = type;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var parameter = _parameter.GetValue(ref context);
        return _type switch
        {
            StringUnaryOperator.Trim => parameter.Trim(),
            StringUnaryOperator.TrimEnd => parameter.TrimEnd(),
            StringUnaryOperator.TrimStart => parameter.TrimStart(),
            StringUnaryOperator.ToUpper => parameter.ToUpper(),
            StringUnaryOperator.ToLower => parameter.ToLower(),
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }

    public override string ToString()
        => $"{_parameter}{Constant.Dote}{_type}";
}