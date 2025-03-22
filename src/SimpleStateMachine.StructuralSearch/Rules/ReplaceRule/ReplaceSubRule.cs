using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

internal class ReplaceSubRule
{
    public readonly PlaceholderParameter Placeholder;
    public readonly IRuleParameter Parameter;
    
    public ReplaceSubRule(PlaceholderParameter placeholder, IRuleParameter parameter)
    {
        Placeholder = placeholder;
        Parameter = parameter;
    }
        
    public override string ToString() 
        => $"{Placeholder}{Constant.Space}{Constant.Should}{Constant.Space}{Parameter}";
}