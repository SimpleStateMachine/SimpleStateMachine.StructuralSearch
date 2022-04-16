namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        FileProperty File { get; }
        bool TryGetPlaceholder(string name, out Placeholder value);

        void AddPlaceholder(Placeholder placeholder);

        Placeholder GetPlaceholder(string name);
    }
}