using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class ChangeUnaryParameter : IStringRuleParameter
{
    private readonly IStringRuleParameter _parameter;
    private readonly ChangeUnaryType _unaryType;

    public ChangeUnaryParameter(IStringRuleParameter parameter, ChangeUnaryType unaryType)
    {
        _parameter = parameter;
        _unaryType = unaryType;
    }

    public string GetValue(ref IParsingContext context)
    {
        var parameter = _parameter.GetValue(ref context);
        return _unaryType switch
        {
            ChangeUnaryType.Trim => parameter.Trim(),
            ChangeUnaryType.TrimEnd => parameter.TrimEnd(),
            ChangeUnaryType.TrimStart => parameter.TrimStart(),
            ChangeUnaryType.ToUpper => parameter.ToUpper(),
            ChangeUnaryType.ToLower => parameter.ToLower(),
            _ => throw new ArgumentOutOfRangeException(nameof(_unaryType).FormatPrivateVar(), _unaryType, null)
        };
    }

    public override string ToString()
        => $"{_parameter}{Constant.Dote}{_unaryType}";
}