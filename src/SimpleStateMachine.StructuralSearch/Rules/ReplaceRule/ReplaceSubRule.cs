using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public class ReplaceSubRule: IContextDependent
{
    public readonly PlaceholderParameter Placeholder;
    public readonly IRuleParameter Parameter;
    
    public ReplaceSubRule(PlaceholderParameter placeholder, IRuleParameter parameter)
    {
        Placeholder = placeholder;
        Parameter = parameter;
    }
        
    public override string ToString()
    {
        return $"{Placeholder}{Constant.Space}{Constant.Should}{Constant.Space}{Parameter}";
    }

    public void SetContext(IParsingContext context)
    {
        Placeholder.SetContext(context);
        Parameter.SetContext(context);
    }
}