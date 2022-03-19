using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class InRule : IRule
    {
        public SubRuleType Type { get; }
        
        public IEnumerable<IRuleParameter> Parameters { get; }
        
        public InRule(SubRuleType type, IEnumerable<IRuleParameter> parameters)
        {
            Type = type;
            
            Parameters = parameters;
        }

        public bool Execute(string value)
        {
            return Parameters.Any(parameter => Equals(value, parameter.GetValue()));
        }
    }
}