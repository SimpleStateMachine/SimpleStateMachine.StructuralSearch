using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderOffsetParameter : IPlaceholderPropertyRuleParameter
{
    private readonly OffsetProperty _property;

    public PlaceholderOffsetParameter(PlaceholderParameter placeholder, OffsetProperty property)
    {
        Placeholder = placeholder;
        _property = property;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = Placeholder.GetPlaceholder(ref context);
        var offset = placeHolder.Offset;

        var value = _property switch
        {
            OffsetProperty.Start => offset.Start,
            OffsetProperty.End => offset.End,
            _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
        };

        return value.ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{PlaceholderProperty.Offset}{Constant.Dote}{_property}";
}