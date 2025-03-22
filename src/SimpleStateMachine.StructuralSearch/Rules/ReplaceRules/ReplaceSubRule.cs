using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

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