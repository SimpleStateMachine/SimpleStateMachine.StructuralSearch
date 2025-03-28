using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderPosition : IPlaceholderProperty
{
    private readonly PlaceholderPositionType _property;
    private readonly PlaceholderPositionSubProperty _subProperty;

    public PlaceholderPosition(PlaceholderParameter placeholder, PlaceholderPositionType property, PlaceholderPositionSubProperty subProperty)
    {
        _property = property;
        _subProperty = subProperty;
        Placeholder = placeholder;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
    {
        var placeholder = Placeholder.GetPlaceholder(ref context);
        return _property switch
        {
            PlaceholderPositionType.Offset => GetPositionValue(placeholder.Offset.Start, placeholder.Offset.End),
            PlaceholderPositionType.Column => GetPositionValue(placeholder.Column.Start, placeholder.Column.End),
            PlaceholderPositionType.Line => GetPositionValue(placeholder.Line.Start, placeholder.Line.End),
            _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
        };
    }
    
    private string GetPositionValue(int start, int end)
    {
        return (_subProperty switch
        {
            PlaceholderPositionSubProperty.Start => start,
            PlaceholderPositionSubProperty.End => end,
            _ => throw new ArgumentOutOfRangeException(nameof(_subProperty).FormatPrivateVar(), _subProperty, null)
        }).ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{_property}{Constant.Dote}{_subProperty}";
}