﻿using System;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

public class PlaceholderLineParameter : IPlaceholderRelatedRuleParameter
{
    private readonly PlaceholderParameter _placeholderParameter;
    private readonly LineProperty _property;

    public PlaceholderLineParameter(PlaceholderParameter parameter, LineProperty property)
    {
        _placeholderParameter = parameter;
        _property = property;
    }
        
    public string Name => _placeholderParameter.Name;
        
    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
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
        => $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.Line}{Constant.Dote}{_property}";
}