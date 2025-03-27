using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class StringFormatParameter : IStringRuleParameter
{
    private readonly string _str;

    public StringFormatParameter(string str)
    {
        _str = str;
    }

    public string GetValue(ref IParsingContext context) => _str;

    public override string ToString()
        => $"{Constant.DoubleQuotes}{_str}{Constant.DoubleQuotes}";
}