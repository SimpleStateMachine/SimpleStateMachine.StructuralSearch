using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class EmptyInput : IInput
    {
        public Result<char, T> Parse<T>(Parser<char, T> parser)
        {
            throw new System.NotImplementedException();
        }
    }
}