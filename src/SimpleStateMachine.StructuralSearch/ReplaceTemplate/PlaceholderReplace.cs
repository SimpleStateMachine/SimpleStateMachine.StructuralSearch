namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class PlaceholderReplace:IReplaceStep
    {
        public string Name { get; }
        
        public PlaceholderReplace(string name)
        {
            Name = name;
        }
        
        public string GetValue()
        {
            return Name;
        }
    }
}