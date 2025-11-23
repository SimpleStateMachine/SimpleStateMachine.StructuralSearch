using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class BinaryOperation(ILogicalOperation left, LogicalBinaryOperator type, ILogicalOperation right)
    : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
    {
        return left.IsApplicableForPlaceholder(placeholderName) || right.IsApplicableForPlaceholder(placeholderName);
    }

    public bool Execute(ref IParsingContext context)
    {
        var leftResult = left.Execute(ref context);
        var rightResult = right.Execute(ref context);

        return type switch
        {
            LogicalBinaryOperator.And => leftResult && rightResult,
            LogicalBinaryOperator.Or => leftResult || rightResult,
            LogicalBinaryOperator.NAND => !(leftResult && rightResult),
            LogicalBinaryOperator.NOR => !(leftResult || rightResult),
            LogicalBinaryOperator.XOR => leftResult ^ rightResult,
            LogicalBinaryOperator.XNOR => leftResult == rightResult,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public override string ToString()
    {
        return $"{left}{Constant.Space}{type}{Constant.Space}{right}";
    }
}