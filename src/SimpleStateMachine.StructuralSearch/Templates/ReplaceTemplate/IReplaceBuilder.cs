using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public interface IReplaceBuilder
    {
        IEnumerable<IRuleParameter> Steps { get; }
        string Build(IParsingContext context);
    }
}