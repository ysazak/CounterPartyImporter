using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImporter.FileParsers
{
    public class FileParserFactory: IFileParserFactory
    {
        static Dictionary<string, Type> ParserTypes = new Dictionary<string, Type>();
        static FileParserFactory()
        {
           CSVParser.ExtensionList().ToList().ForEach(e => ParserTypes.Add(e, typeof(CSVParser)));
           ExcelParser.ExtensionList().ToList().ForEach(e => ParserTypes.Add(e, typeof(ExcelParser)));
        }

        public IFileParser GetParser(string extension)
        {
            Type type;
            if(!ParserTypes.TryGetValue(extension, out type))
            {
                return null;
            }
            return (IFileParser)Activator.CreateInstance(type);
        }

        public static IReadOnlyList<string> SupportedExtensions
        {
            get
            {
                return ParserTypes.Keys.ToList();
            }
        }
    }

    public interface IFileParserFactory
    {
        IFileParser GetParser(string extension);
    }
}
