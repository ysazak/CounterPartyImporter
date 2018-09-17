using DataImporter.Mapper;
using CounterPartyDomain.Models;
using DataImporter.Parsers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace CounterPartyDomain
{
    public class Importer
    {
        private readonly IFileParser parser;
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
            new ComputedPropertyMapping<Company>("CompanyType", fnCompanyType),
            new ColumnToPropertyMapping<Company>("Phone", "Phone"),
            new ColumnToPropertyMapping<Company>("Fax", "Fax"),
        };


        public Importer(IFileParser parser, IMapper<Company, DataTable> mapper)
        {
            this.parser = parser;
            this.mapper = mapper;
        }

        public List<Company> Import(string filePath)
        {
            var dt = parser.ConvertToDataTable(filePath);
            return mapper.Map(dt).ToList();
        }

        /*
        Func<DataRow, object> fnCompanyType = (dr) =>
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

        var mappings = new List<IPropertyMapping<Company>>();
        mappings.Add(new ColumnToPropertyMapping<Company>("ExternalId", "CounterPartID"));
            mappings.Add(new ColumnToPropertyMapping<Company>("TradingName", "Name"));
            mappings.Add(new ComputedPropertyMapping<Company>("CompanyType", fnCompanyType));
            mappings.Add(new ColumnToPropertyMapping<Company>("Phone", "Phone"));
            mappings.Add(new ColumnToPropertyMapping<Company>("Fax", "Fax"));

            var mapper = new DataTableMapper<Company>(mappings);

        */
        //public static bool ToBoolean(this string str)
        //{            
        //    return str.ToLower().Equals("yes");
        //}

    }

}
