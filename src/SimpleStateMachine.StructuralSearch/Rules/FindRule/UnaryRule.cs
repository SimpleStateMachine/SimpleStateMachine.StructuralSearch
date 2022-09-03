using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnaryRule : IRule
    {
        public UnaryRuleType Type { get; }

        public IRule Parameter { get; }

        public UnaryRule(UnaryRuleType type, IRule parameter)
        {
            Type = type;
            Parameter = parameter;
        }

        public bool Execute()
        {
            var result = Parameter.Execute();
            
            return Type switch
            {
                UnaryRuleType.Not => !result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Type}{Constant.Space}{Parameter}";
        }

        public void SetContext(ref IParsingContext context)
        {
            Parameter.SetContext(ref context);
        }
    }
}