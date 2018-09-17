using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Abstractions;

namespace DataImporter.FileParsers
{
    public interface IFileParser
    {
        DataTable ConvertToDataTable(string filePath);
    }

    public abstract class BaseFileParser : IFileParser
    {
        private readonly IFileSystem fileSystem;
        private static List<string> extensions = new List<string>();
        
        protected BaseFileParser() : this(new FileSystem())
        {
        }
        protected BaseFileParser(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public DataTable ConvertToDataTable(string filePath)
        {
            using (Stream stream = fileSystem.File.OpenRead(filePath))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    return ConvertToDataTable(sr);
                }
            }
        }

        public abstract DataTable ConvertToDataTable(StreamReader streamReader);

        public static IEnumerable<string> ExtensionList()
        {
            return new[] { "xls", "xlsx" };
        }
    }
}
