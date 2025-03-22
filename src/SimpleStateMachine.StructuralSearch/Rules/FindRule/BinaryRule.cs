using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

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
            
        return LogicalHelper.Calculate(_type, left, right);
    }
        
    public override string ToString() 
        => $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
}