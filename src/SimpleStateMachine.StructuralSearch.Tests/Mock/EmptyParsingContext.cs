namespace SimpleStateMachine.StructuralSearch.Tests.Mock
{
    public class EmptyParsingContext : IParsingContext
    {
        public bool TryGetPlaceholder(string name, out Placeholder value)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlaceholder(Placeholder placeholder)
        {
            throw new System.NotImplementedException();
        }

        public Placeholder GetPlaceholder(string name)
        {
            return Placeholder.CreateEmpty(this, name, string.Empty);
        }
    }
}