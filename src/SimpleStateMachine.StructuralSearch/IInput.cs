using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IInput
    {
        Result<char, T> ParseBy<T>(Parser<char, T> parser);
        
        string Extension { get; }
        string Path { get; }
        string Name { get; }
        string Data { get; }
        public long Lenght { get; }
    }
}