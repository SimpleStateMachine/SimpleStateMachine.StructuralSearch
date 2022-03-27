namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class TokenReplace : IReplaceStep
    {
        public string Token { get; }
        public TokenReplace(string token)
        {
            Token = token;
        }

        public string GetValue()
        {
            return Token;
        }
    }
}