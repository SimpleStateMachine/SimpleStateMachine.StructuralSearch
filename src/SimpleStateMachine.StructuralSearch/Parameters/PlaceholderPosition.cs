using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderPosition(
    PlaceholderParameter placeholder,
    PlaceholderPositionType property,
    PlaceholderPositionSubProperty subProperty)
    : IPlaceholderProperty
{
    public PlaceholderParameter Placeholder { get; } = placeholder;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => Placeholder.IsApplicableForPlaceholder(placeholderName);

    public string GetValue(ref IParsingContext context)
    {
        var placeholder = Placeholder.GetPlaceholder(ref context);
        return property switch
        {
            PlaceholderPositionType.Offset => GetPositionValue(placeholder.Offset.Start, placeholder.Offset.End),
            PlaceholderPositionType.Column => GetPositionValue(placeholder.Column.Start, placeholder.Column.End),
            PlaceholderPositionType.Line => GetPositionValue(placeholder.Line.Start, placeholder.Line.End),
            _ => throw new ArgumentOutOfRangeException(nameof(property).FormatPrivateVar(), property, null)
        };
    }
    
    private string GetPositionValue(int start, int end)
    {
        return (subProperty switch
        {
            PlaceholderPositionSubProperty.Start => start,
            PlaceholderPositionSubProperty.End => end,
            _ => throw new ArgumentOutOfRangeException(nameof(subProperty).FormatPrivateVar(), subProperty, null)
        }).ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{property}{Constant.Dote}{subProperty}";
}