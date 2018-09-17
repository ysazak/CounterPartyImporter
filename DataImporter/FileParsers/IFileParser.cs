using System.Data;

namespace DataImporter.Parsers
{
    public interface IFileParser
    {
        DataTable ConvertToDataTable(string filePath);
    }
}
