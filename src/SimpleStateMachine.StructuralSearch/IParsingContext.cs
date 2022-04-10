namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        bool TryGetPlaceholder(string name, out Placeholder value);

        void AddPlaceholder(string name, string value);

        Placeholder GetPlaceholder(string name);
    }
}