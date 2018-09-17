using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Text.RegularExpressions;

namespace DataImporter.Parsers
{
    public class CSVParser : IFileParser
    {
        const string DelimeterPattern = ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";
        private readonly IFileSystem fileSystem;

        public CSVParser() : this(new FileSystem())
        {
        }
        public CSVParser(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }
        public DataTable ConvertToDataTable(string filePath)
        {
            using (Stream stream = fileSystem.File.OpenRead(filePath))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    DataTable dt = new DataTable();
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = Regex.Split(sr.ReadLine(), DelimeterPattern);
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
            }

        }
    }
}
