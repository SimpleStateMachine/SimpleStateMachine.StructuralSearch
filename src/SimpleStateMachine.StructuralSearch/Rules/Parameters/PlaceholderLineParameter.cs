using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderLineParameter : IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public LineProperty Property { get; }

        public PlaceholderLineParameter(PlaceholderParameter parameter, LineProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }
        
        public string GetValue()
        {
            var line = PlaceholderParameter.GetPlaceholder().Line;
            
            var value = Property switch
            {
                LineProperty.Start => line.Start,
                LineProperty.End => line.End,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value.ToString();
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}{Constant.Dote}{PlaceholderProperty.Line}{Constant.Dote}{Property}";
        }

        public void SetContext(ref IParsingContext context)
        {
            PlaceholderParameter.SetContext(ref context);
        }
    }
}