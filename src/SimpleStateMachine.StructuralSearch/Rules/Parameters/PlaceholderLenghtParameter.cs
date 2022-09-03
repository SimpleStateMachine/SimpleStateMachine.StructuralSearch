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
        
        public string GetValue()
        {
            return _placeholderParameter.GetPlaceholder().Lenght.ToString();
        }
        
        public override string ToString()
        {
            return $"{_placeholderParameter}{Constant.Dote}{_property}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _placeholderParameter.SetContext(ref context);
        }
    }
}