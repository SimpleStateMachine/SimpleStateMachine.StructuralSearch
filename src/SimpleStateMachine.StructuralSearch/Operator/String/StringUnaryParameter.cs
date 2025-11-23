using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.String;

internal class StringUnaryParameter(IParameter parameter, StringUnaryOperator type) : IParameter
{
    public bool IsApplicableForPlaceholder(string placeholderName)
    {
        return parameter.IsApplicableForPlaceholder(placeholderName);
    }

    public string GetValue(ref IParsingContext context)
    {
        var parameterValue = parameter.GetValue(ref context);

        return type switch
        {
            StringUnaryOperator.Trim => parameterValue.Trim(),
            StringUnaryOperator.TrimEnd => parameterValue.TrimEnd(),
            StringUnaryOperator.TrimStart => parameterValue.TrimStart(),
            StringUnaryOperator.ToUpper => parameterValue.ToUpper(),
            StringUnaryOperator.ToLower => parameterValue.ToLower(),
            _ => throw new ArgumentOutOfRangeException(nameof(type).FormatPrivateVar(), type, null)
        };
    }

    public override string ToString()
    {
        return $"{parameter}{Constant.Dote}{type}";
    }
}