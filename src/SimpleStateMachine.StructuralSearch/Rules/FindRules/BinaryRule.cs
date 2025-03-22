using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

internal class BinaryRule : IFindRule
{
    private readonly BinaryRuleType _type;
    private readonly IFindRule _left;
    private readonly IFindRule _right;

    public BinaryRule(BinaryRuleType type, IFindRule left, IFindRule right)
    {
        _type = type;
        _left = left;
        _right = right;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _left.IsApplicableForPlaceholder(placeholderName) || _right.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var left = _left.Execute(ref context);
        var right = _right.Execute(ref context);

        return _type switch
        {
            BinaryRuleType.And => left && right,
            BinaryRuleType.Or => left || right,
            BinaryRuleType.NAND => !(left && right),
            BinaryRuleType.NOR => !(left || right),
            BinaryRuleType.XOR => left ^ right,
            BinaryRuleType.XNOR => left ==  right,
            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }

    public override string ToString() 
        => $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
}