namespace SimpleStateMachine.StructuralSearch.Tests.Mock
{
    public class EmptyParsingContext : IParsingContext
    {
        public bool TryGetPlaceholder(string name, out string value)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlaceholder(string name, string value)
        {
            throw new System.NotImplementedException();
        }

        public string GetPlaceholder(string name)
        {
            return name;
        }
    }
}