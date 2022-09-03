using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnaryRule : IRule
    {
        private readonly UnaryRuleType _type;
        private readonly IRule _parameter;

        public UnaryRule(UnaryRuleType type, IRule parameter)
        {
            _type = type;
            _parameter = parameter;
        }

        public bool Execute()
        {
            var result = _parameter.Execute();
            
            return _type switch
            {
                UnaryRuleType.Not => !result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{_type}{Constant.Space}{_parameter}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _parameter.SetContext(ref context);
        }
    }
}