using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal class ReplaceBuilder : IReplaceBuilder
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
        => $"{string.Join(string.Empty, Steps)}";
}