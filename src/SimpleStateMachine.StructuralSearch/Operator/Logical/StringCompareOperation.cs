using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class StringCompareOperation(IParameter left, StringCompareOperator @operator, IParameter right)
    : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => left.IsApplicableForPlaceholder(placeholderName) || right.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var leftResult = left.GetValue(ref context);
        var rightResult = right.GetValue(ref context);

        return @operator switch
        {
            StringCompareOperator.Equals => leftResult.Equals(rightResult),
            StringCompareOperator.Contains => leftResult.Contains(rightResult),
            StringCompareOperator.StartsWith => leftResult.StartsWith(rightResult),
            StringCompareOperator.EndsWith => leftResult.EndsWith(rightResult),
            _ => throw new ArgumentOutOfRangeException(nameof(@operator).FormatPrivateVar(), @operator, null)
        };
    }

    public override string ToString()
        => $"{left}{Constant.Space}{@operator}{Constant.Space}{right}";
}