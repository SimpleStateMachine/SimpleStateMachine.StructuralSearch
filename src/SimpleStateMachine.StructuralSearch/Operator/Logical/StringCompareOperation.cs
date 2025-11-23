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
        var left1 = left.GetValue(ref context);
        var right1 = right.GetValue(ref context);

        return @operator switch
        {
            StringCompareOperator.Equals => left1.Equals(right1),
            StringCompareOperator.Contains => left1.Contains(right1),
            StringCompareOperator.StartsWith => left1.StartsWith(right1),
            StringCompareOperator.EndsWith => left1.EndsWith(right1),
            _ => throw new ArgumentOutOfRangeException(nameof(@operator).FormatPrivateVar(), @operator, null)
        };
    }

    public override string ToString()
        => $"{left}{Constant.Space}{@operator}{Constant.Space}{right}";
}