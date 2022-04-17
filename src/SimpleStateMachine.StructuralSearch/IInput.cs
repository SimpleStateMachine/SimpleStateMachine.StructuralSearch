using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IInput
    {
        Result<char, T> Parse<T>(Parser<char, T> parser);
        
        string Extension { get; }
        string Path { get; }
        string Name { get; }
        string Data { get; }
        public long Lenght { get; }
    }
}