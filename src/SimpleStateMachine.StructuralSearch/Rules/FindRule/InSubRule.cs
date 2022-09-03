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
            var result = Arguments.Any(parameter =>
            {
                var value_ = parameter.GetValue();
                var equal = Equals(value, value_);
                return equal;
            });
            return result;
        }
        
        public override string ToString()
        {
            return $"{Parameter}{Constant.Space}{SubRuleType.In}{Constant.Space}{string.Join(Constant.Comma, Arguments.Select(x=>x.ToString()))}";
        }

        public void SetContext(ref IParsingContext context)
        {
            Parameter.SetContext(ref context);

            foreach (var argument in Arguments)
            {
                argument.SetContext(ref context);
            }
        }
    }
}