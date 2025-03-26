using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Placeholder;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderParameter : IPlaceholderPropertyRuleParameter, IStringRuleParameter
{
    public PlaceholderParameter(string name)
    {
        PlaceholderName = name;
    }

    public string PlaceholderName { get; }

    public IPlaceholder GetPlaceholder(ref IParsingContext context)
        => context[PlaceholderName];

    public override string ToString()
        => $"{Constant.PlaceholderSeparator}{PlaceholderName}{Constant.PlaceholderSeparator}";

    public string GetValue(ref IParsingContext context)
        => context[PlaceholderName].Value;

    public PlaceholderParameter Placeholder => this;
}