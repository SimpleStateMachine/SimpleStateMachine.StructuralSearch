using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderLength(PlaceholderParameter placeholder) : IPlaceholderProperty
{
    public PlaceholderParameter Placeholder { get; } = placeholder;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => Placeholder.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var placeholder = Placeholder.GetPlaceholder(ref context);
        return placeholder.Length.ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{Constant.Length}";
}