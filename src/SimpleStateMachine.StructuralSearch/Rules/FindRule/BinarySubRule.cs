using System;
using System.Text.RegularExpressions;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

public class BinarySubRule : IFindRule
{
    private readonly SubRuleType _type;
    private readonly IRuleParameter _left;
    private readonly IRuleParameter _right;
        
    public BinarySubRule(SubRuleType type, IRuleParameter left, IRuleParameter right)
    {
        _type = type;
        _left = left;
        _right = right;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _left.IsApplicableForPlaceholder(placeholderName) || _right.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var left = _left.GetValue(ref context);
        var right = _right.GetValue(ref context);
            
        return _type switch
        {
            SubRuleType.Equals => left.Equals(right),
            SubRuleType.Contains => left.Contains(right),
            SubRuleType.StartsWith => left.StartsWith(right),
            SubRuleType.EndsWith => left.EndsWith(right),
            SubRuleType.Match => Regex.IsMatch(left, right),
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }
        
    public override string ToString() 
        => $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
}