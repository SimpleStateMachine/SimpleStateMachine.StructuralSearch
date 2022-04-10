using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Parsers;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate
{
    public class ReplaceBuilder : IReplaceBuilder
    {
        public IEnumerable<IReplaceStep> Steps { get; }

        public ReplaceBuilder(IEnumerable<IReplaceStep> steps)
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
                    contextDependentStep.SetContext(context);
                }
                
                stringBuilder.Append(step.GetValue());
            }

            var result = stringBuilder.ToString();
            return result;
        }
    }
}