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
            throw new System.NotImplementedException();
        }
    }
}