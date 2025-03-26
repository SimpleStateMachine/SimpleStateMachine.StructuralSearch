using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderLineParameter : IPlaceholderPropertyRuleParameter
{
    private readonly LineProperty _property;

    public PlaceholderLineParameter(PlaceholderParameter placeholder, LineProperty property)
    {
        Placeholder = placeholder;
        _property = property;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = Placeholder.GetPlaceholder(ref context);
        var line = placeHolder.Line;

        var value = _property switch
        {
            LineProperty.Start => line.Start,
            LineProperty.End => line.End,
            _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
        };

        return value.ToString();
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{PlaceholderProperty.Line}{Constant.Dote}{_property}";
}