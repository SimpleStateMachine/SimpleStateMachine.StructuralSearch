using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public class ReplaceSubRule: IContextDependent
{
    public PlaceholderParameter Placeholder { get; }
    public IRuleParameter Parameter { get; }
    
    public ReplaceSubRule(PlaceholderParameter placeholder, IRuleParameter parameter)
    {
        Placeholder = placeholder;
        Parameter = parameter;
    }
        
    public override string ToString()
    {
        return $"{Placeholder}{Constant.Space}{Constant.Should}{Constant.Space}{Parameter}";
    }

    public void SetContext(ref IParsingContext context)
    {
        Placeholder.SetContext(ref context);
        Parameter.SetContext(ref context);
    }
}