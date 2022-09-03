namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class TokenReplace : IReplaceStep
    {
        private readonly string _token;
        
        public TokenReplace(string token)
        {
            _token = token;
        }

        public string GetValue()
        {
            return _token;
        }
        
        public override string ToString()
        {
            return $"{_token}";
        } 
    }
}