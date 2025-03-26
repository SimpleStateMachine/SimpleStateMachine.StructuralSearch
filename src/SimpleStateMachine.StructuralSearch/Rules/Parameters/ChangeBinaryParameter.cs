using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class ChangeBinaryParameter : IStringRuleParameter
{
    private readonly IStringRuleParameter _left;
    private readonly ChangeBinaryType _type;
    private readonly IStringRuleParameter _right;

    public ChangeBinaryParameter(IStringRuleParameter left, ChangeBinaryType type, IStringRuleParameter right)
    {
        _left = left;
        _type = type;
        _right = right;
    }

    public string GetValue(ref IParsingContext context)
    {
        var left = _left.GetValue(ref context);
        var right = _right.GetValue(ref context);
        return _type switch
        {
            ChangeBinaryType.RemoveSubStr => left.Replace(right, string.Empty),
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }

    public override string ToString()
        => $"{_left}{Constant.Dote}{_type}{Constant.LeftParenthesis}{_right}{Constant.RightParenthesis}";
}