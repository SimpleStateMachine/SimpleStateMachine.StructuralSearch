namespace SimpleStateMachine.StructuralSearch.Rules;

internal class PlaceholderParameter : IPlaceholderRelatedRuleParameter
{
    public PlaceholderParameter(string name)
    {
        Name = name;
    }
        
    public string Name { get; }
        
    public string GetValue(ref IParsingContext context) 
        => GetPlaceholder(ref context).Value;

    public IPlaceholder GetPlaceholder(ref IParsingContext context)
        => context[Name];

    public override string ToString() 
        => $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
}