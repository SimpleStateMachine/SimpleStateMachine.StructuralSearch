using Pidgin;

namespace SimpleStateMachine.StructuralSearch;

public interface IInput
{
    Result<char, T> ParseBy<T>(Parser<char, T> parser);
    void Replace(Match<string> match, string value);
        
    string Extension { get; }
    string Path { get; }
    string Name { get; }
    string Data { get; }
    public long Lenght { get; }
}