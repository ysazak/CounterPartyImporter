using DataImporter.Mapper;
using CounterPartyDomain.Models;
using DataImporter.FileParsers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.IO;
using DataImporter.Utils;

namespace CounterPartyDomain
{
    public class Importer
    {
        private IFileParser parser;
        private readonly FileParserFactory fileParserFactory;
        private readonly IMapper<Company, DataTable> mapper;

        static Func<DataRow, object> fnCompanyType = (dr) =>
        {
            if (dr["IsBuyer"].ToString() == null && dr["IsSeller"].ToString() == null)
            {
                throw new Exception("Required columns not found");
            }
            bool isBuyer = false;
            isBuyer = string.Equals(dr["IsBuyer"].ToString(), "yes", StringComparison.InvariantCultureIgnoreCase);

            bool isSeller = false;
            isSeller = string.Equals(dr["IsSeller"].ToString(), "yes", StringComparison.InvariantCultureIgnoreCase);

            if (isBuyer && isSeller) return CompanyType.BuyerAndSeller;
            if (isSeller) return CompanyType.Seller;
            if (isBuyer) return CompanyType.Buyer;
            return CompanyType.None;

        };

        List<IPropertyMapping<Company>> mappings = new List<IPropertyMapping<Company>> {
            new ColumnToPropertyMapping<Company>("ExternalId", "CounterPartID"),
            new ColumnToPropertyMapping<Company>("TradingName", "Name"),
            new ColumnToPropertyMapping<Company>("LegalName", "Name"),
            new ComputedPropertyMapping<Company>("CompanyType", fnCompanyType),
            new ColumnToPropertyMapping<Company>("Phone", "Phone"),
            new ColumnToPropertyMapping<Company>("Fax", "Fax"),
        };


        public Importer()
        {
            fileParserFactory = new FileParserFactory();
            this.mapper = new DataTableMapper<Company>(mappings);
        }

        public List<Company> Import(string filePath)
        {
            string extension = IOUtils.GetExtensionWithoutDot(filePath).ToLower();
            parser = fileParserFactory.GetParser(extension);
            if(parser == null)
            {
                throw new NotSupportedException($"{extension} is not supported");
            }
            var dt = parser.ConvertToDataTable(filePath);
            return mapper.Map(dt).ToList();
        }
    }

}



