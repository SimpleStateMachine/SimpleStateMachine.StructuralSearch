using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderInput : IPlaceholderProperty
{
    private readonly string _propertyName;

    public PlaceholderInput(PlaceholderParameter parameter, string propertyName)
    {
        Placeholder = parameter;
        _propertyName = propertyName;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
        => context.Input.GetProperty(_propertyName);

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{Constant.Input}{Constant.Dote}{_propertyName}";
}