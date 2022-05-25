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
            return $"{Constant.DoubleQuotes}{string.Join(string.Empty, Parameters.Select(x=> x.ToString()))}{Constant.DoubleQuotes}";
        }

        public void SetContext(ref IParsingContext context)
        {
            foreach (var parameter in Parameters)
            {
                parameter.SetContext(ref context);
            }
        }
    }
}