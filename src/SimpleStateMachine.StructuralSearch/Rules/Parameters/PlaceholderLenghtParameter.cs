namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderLenghtParameter: IRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly PlaceholderProperty _property;

        public PlaceholderLenghtParameter(PlaceholderParameter parameter, PlaceholderProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }
        
        public string GetValue(ref IParsingContext context)
        {
            var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
            return placeHolder.Lenght.ToString();
        }
        
        public override string ToString() 
            => $"{_placeholderParameter}{Constant.Dote}{_property}";
    }
}