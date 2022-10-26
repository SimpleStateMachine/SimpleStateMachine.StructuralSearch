using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class BinaryRule : IRule
    {
        private readonly BinaryRuleType _type;
        private readonly IRule _left;
        private readonly IRule _right;

        public BinaryRule(BinaryRuleType type, IRule left, IRule right)
        {
            _type = type;
            _left = left;
            _right = right;
        }

        public bool Execute(ref IParsingContext context)
        {
            var left = _left.Execute(ref context);
            var right = _right.Execute(ref context);
            
            return LogicalHelper.Calculate(_type, left, right);
        }
        
        public override string ToString()
        {
            return $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
        }
    }
}