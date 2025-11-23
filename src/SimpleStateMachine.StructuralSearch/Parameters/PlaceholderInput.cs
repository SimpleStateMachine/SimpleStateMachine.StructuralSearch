using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderInput(PlaceholderParameter parameter, string propertyName) : IPlaceholderProperty
{
    public PlaceholderParameter Placeholder { get; } = parameter;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => Placeholder.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
        => context.Input.GetProperty(propertyName);

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{Constant.Input}{Constant.Dote}{propertyName}";
}