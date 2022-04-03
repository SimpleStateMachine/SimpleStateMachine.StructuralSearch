namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        bool TryGetPlaceholder(string name, out string value);

        void AddPlaceholder(string name, string value);

        string GetPlaceholder(string name);
    }
}