namespace SimpleStateMachine.StructuralSearch.Tests.Mock
{
    public class EmptyParsingContext : IParsingContext
    {
        public bool TryGetPlaceholder(string name, out Placeholder.Placeholder? value)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlaceholder(string name, string value)
        {
            throw new System.NotImplementedException();
        }

        public Placeholder.Placeholder GetPlaceholder(string name)
        {
            return new Placeholder.Placeholder(this, name, string.Empty);
        }
    }
}