using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IInput
    {
        Result<char, T> Parse<T>(Parser<char, T> parser);
    }
}