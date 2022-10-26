using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class ReplaceBuilder : IReplaceBuilder
    {
        public static readonly EmptyReplaceBuilder Empty = new ();
        
        public IEnumerable<IRuleParameter> Steps { get; }

        public ReplaceBuilder(IEnumerable<IRuleParameter> steps)
        {
            Steps = steps;
        }

        public string Build(ref IParsingContext context)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var step in Steps)
            {
                stringBuilder.Append(step.GetValue(ref context));
            }

            var result = stringBuilder.ToString();
            return result;
        }
        
        public override string ToString()
        {
            return $"{string.Join(string.Empty, Steps)}";
        } 
    }
}