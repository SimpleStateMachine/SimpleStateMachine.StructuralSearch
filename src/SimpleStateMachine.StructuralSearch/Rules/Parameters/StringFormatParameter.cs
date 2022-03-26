using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class StringFormatParameter : IRuleParameter
    {
        public IEnumerable<IRuleParameter> Parameters { get; }

        public StringFormatParameter(IEnumerable<IRuleParameter> parameters)
        {
            Parameters = parameters;
        }

        public string GetValue()
        {
            return string.Join(string.Empty, Parameters.Select(x => x.GetValue()));
        }
        
        public override string ToString()
        {
            return $"{string.Join(Constant.Space, Parameters.Select(x=> x.ToString()))}";
        } 
    }
}