namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate;

public class TokenReplace : IReplaceStep
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