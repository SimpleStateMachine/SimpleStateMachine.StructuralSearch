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

        public string Extension => FileInfo.Extension;
        public string Path => System.IO.Path.GetFullPath(FileInfo.FullName);
        public string Name => System.IO.Path.GetFileNameWithoutExtension(FileInfo.Name);
        public string Data => FileInfo.OpenText().ReadToEnd();
        public long Lenght => FileInfo.Length;
    }
}