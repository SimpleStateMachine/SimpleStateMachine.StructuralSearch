using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Placeholder;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderParameter : IPlaceholderRelatedRuleParameter
{
    public PlaceholderParameter(string name)
    {
        PlaceholderName = name;
    }
        
    public string PlaceholderName { get; }
        
    public string GetValue(ref IParsingContext context) 
        => GetPlaceholder(ref context).Value;

    public IPlaceholder GetPlaceholder(ref IParsingContext context)
        => context[PlaceholderName];

    public override string ToString() 
        => $"{Constant.PlaceholderSeparator}{PlaceholderName}{Constant.PlaceholderSeparator}";
}