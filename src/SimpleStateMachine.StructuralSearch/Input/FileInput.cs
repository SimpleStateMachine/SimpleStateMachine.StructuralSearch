using System.IO;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class FileInput : IInput
    {
        public readonly FileInfo FileInfo;
        
        public FileInput(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public Result<char, T> ParseBy<T>(Parser<char, T> parser) 
            => parser.Parse(FileInfo.OpenText());

        public void Replace(Match<string> match, string value)
        {
            var text = File.ReadAllText(Path);
            text = text.Replace(match.Value, value);
            File.WriteAllText(Path, text);

            // using var valueReader = new StringReader(value);
            // using var streamWriter = new StreamWriter(FileInfo.FullName);
            // var emptyStart = match.Offset.Start + match.Lenght;
            // var emptyEnd = match.Offset.End;
            // valueReader.CopyPartTo(streamWriter, match.Offset.Start, match.Lenght);

            //return (emptyStart, emptyEnd);
        }

        public string Extension => FileInfo.Extension;
        public string Path => System.IO.Path.GetFullPath(FileInfo.FullName);
        public string Name => System.IO.Path.GetFileNameWithoutExtension(FileInfo.Name);
        public string Data => FileInfo.OpenText().ReadToEnd();
        public long Lenght => FileInfo.Length;
    }
}