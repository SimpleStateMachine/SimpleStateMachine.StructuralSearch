using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderColumnParameter : IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public ColumnProperty Property { get; }

        public PlaceholderColumnParameter(PlaceholderParameter parameter, ColumnProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }
        
        public string GetValue()
        {
            var column = PlaceholderParameter.GetPlaceholder().Column;
            
            var value = Property switch
            {
                ColumnProperty.Start => column.Start,
                ColumnProperty.End => column.End,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value.ToString();
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}{Constant.Dote}{PlaceholderProperty.Column}{Constant.Dote}{Property}";
        }

        public void SetContext(ref IParsingContext context)
        {
            PlaceholderParameter.SetContext(ref context);
        }
    }
}