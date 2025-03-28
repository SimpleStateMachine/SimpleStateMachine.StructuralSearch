using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class BinaryOperation : ILogicalOperation
{
    private readonly LogicalBinaryOperator _type;
    private readonly ILogicalOperation _left;
    private readonly ILogicalOperation _right;

    public BinaryOperation(ILogicalOperation left, LogicalBinaryOperator type, ILogicalOperation right)
    {
        _type = type;
        _left = left;
        _right = right;
    }

    public bool Execute(ref IParsingContext context)
    {
        var left = _left.Execute(ref context);
        var right = _right.Execute(ref context);

        return _type switch
        {
            LogicalBinaryOperator.And => left && right,
            LogicalBinaryOperator.Or => left || right,
            LogicalBinaryOperator.NAND => !(left && right),
            LogicalBinaryOperator.NOR => !(left || right),
            LogicalBinaryOperator.XOR => left ^ right,
            LogicalBinaryOperator.XNOR => left == right,
            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }

    public override string ToString()
        => $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
}