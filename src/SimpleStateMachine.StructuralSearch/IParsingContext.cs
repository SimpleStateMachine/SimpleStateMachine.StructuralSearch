namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        bool TryGetPlaceholder(string name, out Placeholder.Placeholder? value);

        void AddPlaceholder(string name, string value);

        Placeholder.Placeholder GetPlaceholder(string name);
    }
}