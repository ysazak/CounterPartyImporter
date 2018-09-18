using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataImporter.Utils
{
    public static class IOUtils
    {
        public static string GetExtensionWithoutDot(string path)
        {
            return Path.GetExtension(path).TrimStart('.');
        }
    }
}
