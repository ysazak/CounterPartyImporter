using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Text.RegularExpressions;

namespace DataImporter.FileParsers
{
    public class ExcelParser : BaseFileParser
    {
        public ExcelParser()
        {
        }

        public ExcelParser(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public override DataTable ConvertToDataTable(StreamReader streamReader)
        {
            // Future development
            throw new NotImplementedException();
        }

        public static new IEnumerable<string> ExtensionList()
        {
            return new[] { "xls", "xlsx" };
        }
    }
}
