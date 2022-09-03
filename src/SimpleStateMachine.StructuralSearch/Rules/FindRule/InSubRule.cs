using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class InSubRule : IRule
    {
        private readonly IRuleParameter _parameter;

        private readonly IEnumerable<IRuleParameter> _arguments;
        
        public InSubRule(IRuleParameter parameter, IEnumerable<IRuleParameter> arguments)
        {
            _parameter = parameter;
            
            _arguments = arguments;
        }

        public bool Execute()
        {
            var value = _parameter.GetValue();
            var result = _arguments.Any(parameter =>
            {
                var valueForResult = parameter.GetValue();
                var equal = Equals(value, valueForResult);
                return equal;
            });
            return result;
        }
        
        public override string ToString()
        {
            return $"{_parameter}{Constant.Space}{SubRuleType.In}{Constant.Space}{string.Join(Constant.Comma, _arguments.Select(x=>x.ToString()))}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _parameter.SetContext(ref context);

            foreach (var argument in _arguments)
            {
                argument.SetContext(ref context);
            }
        }
    }
}