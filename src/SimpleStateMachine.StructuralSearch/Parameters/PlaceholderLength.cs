using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderLength : IPlaceholderProperty
{
    public PlaceholderLength(PlaceholderParameter placeholder)
    {
        Placeholder = placeholder;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
    {
        var placeholder = Placeholder.GetPlaceholder(ref context);
        return placeholder.Lenght.ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{Constant.Length}";
}