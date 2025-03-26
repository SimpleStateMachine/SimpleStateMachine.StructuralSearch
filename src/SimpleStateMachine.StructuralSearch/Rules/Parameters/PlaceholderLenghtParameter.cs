using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderLenghtParameter : IPlaceholderPropertyRuleParameter
{
    private readonly PlaceholderProperty _property;

    public PlaceholderLenghtParameter(PlaceholderParameter placeholder, PlaceholderProperty property)
    {
        Placeholder = placeholder;
        _property = property;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = Placeholder.GetPlaceholder(ref context);
        return placeHolder.Lenght.ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{_property}";
}