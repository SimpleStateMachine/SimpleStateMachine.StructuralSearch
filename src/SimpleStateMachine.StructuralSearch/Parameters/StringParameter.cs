using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringParameter(string value) : IParameter
{
    public static readonly StringParameter Empty = new(string.Empty);

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public string GetValue(ref IParsingContext context) 
        => value;

    public override string ToString()
        => value;
}