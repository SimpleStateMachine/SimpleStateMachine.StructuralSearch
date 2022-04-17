using System.IO;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class FileInput : IInput
    {
        public FileInput(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
        
        public readonly FileInfo FileInfo;
        
        public Result<char, T> Parse<T>(Parser<char, T> parser)
        {
            return parser.Parse(FileInfo.OpenText());
        }
    }
}