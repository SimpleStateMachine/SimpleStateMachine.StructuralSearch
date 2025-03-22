using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal class TokenReplace : IReplaceStep
{
    private readonly string _token;
        
    public TokenReplace(string token)
    {
        _token = token;
    }

    public string GetValue(ref IParsingContext context) 
        => _token;

    public override string ToString() 
        => $"{_token}";
}