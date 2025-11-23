using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class BinaryOperation(ILogicalOperation left, LogicalBinaryOperator type, ILogicalOperation right)
    : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => left.IsApplicableForPlaceholder(placeholderName) || right.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var left1 = left.Execute(ref context);
        var right1 = right.Execute(ref context);

        return type switch
        {
            LogicalBinaryOperator.And => left1 && right1,
            LogicalBinaryOperator.Or => left1 || right1,
            LogicalBinaryOperator.NAND => !(left1 && right1),
            LogicalBinaryOperator.NOR => !(left1 || right1),
            LogicalBinaryOperator.XOR => left1 ^ right1,
            LogicalBinaryOperator.XNOR => left1 == right1,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public override string ToString()
        => $"{left}{Constant.Space}{type}{Constant.Space}{right}";
}