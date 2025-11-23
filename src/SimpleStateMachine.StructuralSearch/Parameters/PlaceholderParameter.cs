using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class PlaceholderParameter(string name) : IPlaceholderProperty
{
    private string PlaceholderName { get; } = name;

    public IPlaceholder GetPlaceholder(ref IParsingContext context)
        => context[PlaceholderName];

    public override string ToString()
        => $"{Constant.PlaceholderSeparator}{PlaceholderName}{Constant.PlaceholderSeparator}";

    public bool IsApplicableForPlaceholder(string placeholderName)
        => Equals(PlaceholderName, placeholderName);

    public string GetValue(ref IParsingContext context)
        => context[PlaceholderName].Value;

    public PlaceholderParameter Placeholder => this;
}