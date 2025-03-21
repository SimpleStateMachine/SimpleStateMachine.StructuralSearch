namespace SimpleStateMachine.StructuralSearch.Rules;

public class PlaceholderParameter : IPlaceholderRelatedRuleParameter
{
    public PlaceholderParameter(string name)
    {
        Name = name;
    }
        
    public string Name { get; }
        
    public string GetValue(ref IParsingContext context) 
        => GetPlaceholder(ref context).Value;

    public IPlaceholder GetPlaceholder(ref IParsingContext context)
        => context.GetPlaceholder(Name);

    public override string ToString() 
        => $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
}