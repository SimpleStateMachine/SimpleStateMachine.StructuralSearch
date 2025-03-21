using System;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.Helper;

public static class LogicalHelper
{
    public static bool Calculate(BinaryRuleType type, bool left, bool right) 
        => type switch
        {
            BinaryRuleType.And => left && right,
            BinaryRuleType.Or => left || right,
            BinaryRuleType.NAND => !(left && right),
            BinaryRuleType.NOR => !(left || right),
            BinaryRuleType.XOR => left ^ right,
            BinaryRuleType.XNOR => left ==  right,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}