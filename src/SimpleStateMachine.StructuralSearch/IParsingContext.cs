namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        bool TryGetPlaceholder(string name, out Placeholder value);

        void AddPlaceholder(Placeholder placeholder);

        Placeholder GetPlaceholder(string name);
    }
}