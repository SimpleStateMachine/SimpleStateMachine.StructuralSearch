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
            throw new System.NotImplementedException();
        }
    }
}