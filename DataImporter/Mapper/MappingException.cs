using System;
using System.Text;

namespace DataImporter.Mapper
{
    public class MappingException: Exception
    {
        private readonly string propertyName;
        private readonly string columnName;
        private readonly string message;
        private readonly Exception exception;

        public MappingException(string message)
        {
            this.message = message;
        }

        public MappingException(string message, string propertyName): this(message)
        {
            this.propertyName = propertyName;
        }

        public MappingException(Exception ex, string propertyName): this(ex)
        {
            this.propertyName = propertyName;
        }

        public MappingException(Exception ex, string propertyName, string columnName):this (ex, propertyName)
        {
            this.columnName = columnName;
        }

        public MappingException(Exception ex)
        {
            this.message = ex?.Message;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"Mapping error: {message}");
            if (!string.IsNullOrEmpty(columnName))
            {
                sb.Append($"Property name= {propertyName}");
            }
            if (!string.IsNullOrEmpty(columnName))
            {
                sb.Append($", Column name= {propertyName}");
            }
            sb.Append($")");
            return sb.ToString();
        }
    }

}
