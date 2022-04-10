namespace SimpleStateMachine.StructuralSearch.Tests.Mock
{
    public class EmptyParsingContext : IParsingContext
    {
        public bool TryGetPlaceholder(string name, out Placeholder value)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlaceholder(string name, string value)
        {
            throw new System.NotImplementedException();
        }

        public Placeholder GetPlaceholder(string name)
        {
            return new Placeholder(this, name, string.Empty);
        }
    }
}