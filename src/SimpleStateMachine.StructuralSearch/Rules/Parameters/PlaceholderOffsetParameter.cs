using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderOffsetParameter : IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public OffsetProperty Property { get; }

        public PlaceholderOffsetParameter(PlaceholderParameter parameter, OffsetProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }
        public string GetValue()
        {
            var offset = PlaceholderParameter.GetPlaceholder().Offset;
            
            var value = Property switch
            {
                OffsetProperty.Start => offset.Start,
                OffsetProperty.End => offset.End,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value.ToString();
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}{Constant.Dote}{PlaceholderProperty.Offset}{Constant.Dote}{Property}";
        } 
    }
}