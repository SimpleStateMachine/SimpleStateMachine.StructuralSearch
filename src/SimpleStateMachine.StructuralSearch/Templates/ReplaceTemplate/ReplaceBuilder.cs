using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class ReplaceBuilder : IReplaceBuilder
    {
        public static readonly IReplaceBuilder Empty = new EmptyReplaceBuilder();
        
        public IEnumerable<IRuleParameter> Steps { get; }

        public ReplaceBuilder(IEnumerable<IRuleParameter> steps)
        {
            Steps = steps;
        }

        public string Build(IParsingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var step in Steps)
            {
                if (step is IContextDependent contextDependentStep)
                {
                    contextDependentStep.SetContext(ref context);
                }
                
                stringBuilder.Append(step.GetValue());
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