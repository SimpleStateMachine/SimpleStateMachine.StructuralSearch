using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.String;

internal class StringUnaryParameter(IParameter parameter, StringUnaryOperator type) : IParameter
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameter.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var parameter1 = parameter.GetValue(ref context);
        return type switch
        {
            StringUnaryOperator.Trim => parameter1.Trim(),
            StringUnaryOperator.TrimEnd => parameter1.TrimEnd(),
            StringUnaryOperator.TrimStart => parameter1.TrimStart(),
            StringUnaryOperator.ToUpper => parameter1.ToUpper(),
            StringUnaryOperator.ToLower => parameter1.ToLower(),
            _ => throw new ArgumentOutOfRangeException(nameof(type).FormatPrivateVar(), type, null)
        };
    }

    public override string ToString()
        => $"{parameter}{Constant.Dote}{type}";
}