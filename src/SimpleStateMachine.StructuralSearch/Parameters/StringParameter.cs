using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal class StringParameter : IParameter
{
    public static StringParameter Empty = new StringParameter(string.Empty);

    private readonly string _value;

    public StringParameter(string value)
    {
        _value = value;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public string GetValue(ref IParsingContext context) 
        => _value;

    public override string ToString()
        => _value;
}