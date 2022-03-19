using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class BinaryRule : IRule
    {
        public BinaryRuleType Type { get; }

        public IRule Left { get; }

        public IRule Right { get; }

        public BinaryRule(BinaryRuleType type, IRule left, IRule right)
        {
            Type = type;
            Left = left;
            Right = right;
        }

        public bool Execute(string value)
        {
            var left = Left.Execute(value);
            var right = Right.Execute(value);
            
            return LogicalHelper.Calculate(Type, left, right);
        }
    }
}