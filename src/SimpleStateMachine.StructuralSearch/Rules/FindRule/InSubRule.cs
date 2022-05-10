using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class InSubRule : IRule
    {
        public IRuleParameter Parameter { get; }
        
        public IEnumerable<IRuleParameter> Arguments { get; }
        
        public InSubRule(IRuleParameter parameter, IEnumerable<IRuleParameter> arguments)
        {
            Parameter = parameter;
            
            Arguments = arguments;
        }

        public bool Execute()
        {
            var value = Parameter.GetValue();
            return Arguments.Any(parameter => Equals(value, parameter.GetValue()));
        }
        
        public override string ToString()
        {
            return $"{Parameter}{Constant.Space}{SubRuleType.In}{Constant.Space}{string.Join(Constant.Comma, Arguments.Select(x=>x.ToString()))}";
        }  
    }
}