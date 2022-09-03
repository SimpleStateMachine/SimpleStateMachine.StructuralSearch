using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderLineParameter : IRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly LineProperty _property;

        public PlaceholderLineParameter(PlaceholderParameter parameter, LineProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }
        
        public string GetValue()
        {
            var line = _placeholderParameter.GetPlaceholder().Line;
            
            var value = _property switch
            {
                LineProperty.Start => line.Start,
                LineProperty.End => line.End,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value.ToString();
        }
        
        public override string ToString()
        {
            return $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.Line}{Constant.Dote}{_property}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _placeholderParameter.SetContext(ref context);
        }
    }
}