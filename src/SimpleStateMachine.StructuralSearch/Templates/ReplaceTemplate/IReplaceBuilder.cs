using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate
{
    public interface IReplaceBuilder
    {
        IEnumerable<IReplaceStep> Steps { get; }
        string Build(IParsingContext context);
    }
}