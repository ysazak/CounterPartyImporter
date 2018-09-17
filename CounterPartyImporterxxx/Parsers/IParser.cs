using System.Data;

namespace CounterPartyImporter.Parsers
{
    public interface IParser
    {
        DataTable ConvertToDataTable(string filePath);
    }
}
