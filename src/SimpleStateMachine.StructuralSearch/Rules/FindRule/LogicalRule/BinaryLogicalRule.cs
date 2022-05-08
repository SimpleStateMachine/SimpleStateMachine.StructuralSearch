using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

public class BinaryLogicalRule : ILogicalRule
{
    public BinaryRuleType Type { get; }

    public ILogicalRule Left { get; }

    public ILogicalRule Right { get; }

    public BinaryLogicalRule(BinaryRuleType type, ILogicalRule left, ILogicalRule right)
    {
        Type = type;
        Left = left;
        Right = right;
    }

    public bool Execute()
    {
        var left = Left.Execute();
        var right = Right.Execute();

        return LogicalHelper.Calculate(Type, left, right);
    }

    public override string ToString()
    {
        return $"{Left}{Constant.Space}{Type}{Constant.Space}{Right}";
    }
}