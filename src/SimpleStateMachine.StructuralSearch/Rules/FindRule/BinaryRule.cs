using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRule
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
        
        public override string ToString()
        {
            return $"{Left}{Constant.Space}{Type}{Constant.Space}{Right}";
        } 
    }
}