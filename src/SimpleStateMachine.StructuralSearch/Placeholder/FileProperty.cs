using System.IO;

namespace SimpleStateMachine.StructuralSearch
{
    public class FileProperty
    {
        public FileProperty(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            StreamReader = new StreamReader(fileInfo.OpenRead());
        }

        public readonly FileInfo FileInfo;
        public readonly string Path;
        public readonly string Data;
        public readonly StreamReader StreamReader;
        public string Name => FileInfo.Name;
        public string Directory => FileInfo.FullName;
        public long Lenght => FileInfo.Length;

        public static FileProperty FromFile(string path)
        {
            return new FileProperty(new FileInfo(path));
        }

        public static readonly FileProperty Empty = new FileProperty(null);
    }
}