namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class PlaceholderReplace : IReplaceStep
    {
        private readonly string _name;

        public PlaceholderReplace(string name)
        {
            _name = name;
        }

        public string GetValue(ref IParsingContext context)
        {
            var placeHolder = context.GetPlaceholder(_name);
            return placeHolder.Value;
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{_name}{Constant.PlaceholderSeparator}";
        }  
    }
}