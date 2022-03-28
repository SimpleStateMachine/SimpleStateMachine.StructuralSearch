using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class ReplaceBuilder : IReplaceBuilder
    {
        private ParsingContext _context;
        public IEnumerable<IReplaceStep> Steps { get; }

        public ReplaceBuilder(ParsingContext context, IEnumerable<IReplaceStep> steps)
        {
            Steps = steps;
            _context = context;
        }

        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var step in Steps)
            {
                if (step is IContextDependent contextDependentStep)
                {
                    contextDependentStep.SetContext(_context);
                }
                
                stringBuilder.Append(step.GetValue());
            }

            var result = stringBuilder.ToString();
            return result;
        }
    }
}