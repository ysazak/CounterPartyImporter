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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CounterPartyImporter.Web.BL
{
    public class CounterPartyImporter
    {
        private IFileParser parser;
        private readonly IMapper<Company, DataTable> mapper;
        private readonly IFileParserFactory fileParserFactory;
        static Func<DataRow, object> fnCompanyType = (dr) =>
        {
            if (dr["IsBuyer"].ToString() == null && dr["IsSeller"].ToString() == null)
            {
                throw new MappingException("Required columns are not found");
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


        public CounterPartyImporter(IServiceProvider services, IFileParserFactory fileParserFactory)
        {
            this.mapper = services.GetService<IMapper<Company, DataTable>>();
            this.fileParserFactory = fileParserFactory;
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
            return mapper.Map(mappings, dt).ToList();
        }
    }

}



