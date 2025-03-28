using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class StringCompareOperation : ILogicalOperation
{
    private readonly StringCompareOperator _operator;
    private readonly IParameter _left;
    private readonly IParameter _right;

    public StringCompareOperation(IParameter left, StringCompareOperator @operator, IParameter right)
    {
        _operator = @operator;
        _left = left;
        _right = right;
    }

    public bool Execute(ref IParsingContext context)
    {
        var left = _left.GetValue(ref context);
        var right = _right.GetValue(ref context);

        return _operator switch
        {
            StringCompareOperator.Equals => left.Equals(right),
            StringCompareOperator.Contains => left.Contains(right),
            StringCompareOperator.StartsWith => left.StartsWith(right),
            StringCompareOperator.EndsWith => left.EndsWith(right),
            _ => throw new ArgumentOutOfRangeException(nameof(_operator).FormatPrivateVar(), _operator, null)
        };
    }

    public override string ToString()
        => $"{_left}{Constant.Space}{_operator}{Constant.Space}{_right}";
}