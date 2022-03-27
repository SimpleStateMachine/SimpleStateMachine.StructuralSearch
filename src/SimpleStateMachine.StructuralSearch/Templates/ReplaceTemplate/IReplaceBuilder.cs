using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public interface IReplaceBuilder
    {
        IEnumerable<IReplaceStep> Steps { get; }
        string Build();
    }
}