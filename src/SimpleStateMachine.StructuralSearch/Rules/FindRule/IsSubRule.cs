using System;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class IsSubRule : IRule
    {
        private readonly PlaceholderType _argument;
        private readonly IRuleParameter _parameter;
        
        public IsSubRule(IRuleParameter parameter, PlaceholderType argument)
        {
            _parameter = parameter;
            _argument = argument;
        }

        public bool Execute()
        {
            var value = _parameter.GetValue();
            
            return _argument switch
            {
                PlaceholderType.Var => CommonParser.Identifier.TryParse(value, out _),
                PlaceholderType.Int => int.TryParse(value, out _),
                PlaceholderType.Double => double.TryParse(value, out _),
                PlaceholderType.DateTime => DateTime.TryParse(value, out _),
                PlaceholderType.Guid => Guid.TryParse(value, out _),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{_parameter}{Constant.Space}{SubRuleType.Is}{Constant.Space}{_argument}";
        }

        public void SetContext(IParsingContext context)
        {
            _parameter.SetContext(context);
        }
    }
}