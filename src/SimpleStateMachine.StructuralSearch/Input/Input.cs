using System.IO;

namespace SimpleStateMachine.StructuralSearch
{
    public static class Input
    {
        public static readonly IInput Empty = new EmptyInput();
        
        public static IInput String(string input) => new StringInput(input);
        
        public static IInput File(FileInfo fileInfo) => new FileInput(fileInfo);
    }
}