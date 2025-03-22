namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate;

internal class PlaceholderReplace : IReplaceStep
{
    private readonly string _name;

    public PlaceholderReplace(string name)
    {
        _name = name;
    }

    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = context[_name];
        return placeHolder.Value;
    }
        
    public override string ToString() 
        => $"{Constant.PlaceholderSeparator}{_name}{Constant.PlaceholderSeparator}";
}