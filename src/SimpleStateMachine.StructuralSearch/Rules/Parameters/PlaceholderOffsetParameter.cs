using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderOffsetParameter : IRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly OffsetProperty _property;

        public PlaceholderOffsetParameter(PlaceholderParameter parameter, OffsetProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }
        public string GetValue()
        {
            var offset = _placeholderParameter.GetPlaceholder().Offset;
            
            var value = _property switch
            {
                OffsetProperty.Start => offset.Start,
                OffsetProperty.End => offset.End,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value.ToString();
        }
        
        public override string ToString()
        {
            return $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.Offset}{Constant.Dote}{_property}";
        }

        public void SetContext(IParsingContext context)
        {
            _placeholderParameter.SetContext(context);
        }
    }
}