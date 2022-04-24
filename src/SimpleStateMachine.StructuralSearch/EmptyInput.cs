using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class EmptyInput : IInput
    {
        public Result<char, T> ParseBy<T>(Parser<char, T> parser)
        {
            throw new System.NotImplementedException();
        }

        public string Extension => string.Empty;
        public string Path => string.Empty;
        public string Name => string.Empty;
        public string Data => string.Empty;
        public long Lenght => 0;
    }
}