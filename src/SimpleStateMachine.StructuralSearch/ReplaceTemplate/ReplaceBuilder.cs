using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class ReplaceBuilder : IReplaceBuilder
    {
        public IEnumerable<IReplaceStep> Steps { get; }

        public ReplaceBuilder(IEnumerable<IReplaceStep> steps)
        {
            Steps = steps;
        }

        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var step in Steps)
            {
                stringBuilder.Append(step.GetValue());
            }

            var result = stringBuilder.ToString();
            return result;
        }
    }
}