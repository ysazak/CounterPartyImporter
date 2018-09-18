using DataImporter;
using DataImporter.Mapper;
using CounterPartyDomain.Models;
using DataImporter.FileParsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CounterPartyDomain;
using DataImporter.FileParsers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Func<DataRow, object> fnCompanyType = (dr) =>
            //{
            //    if (dr["IsBuyer"].ToString() == null && dr["IsSeller"].ToString() == null)
            //    {
            //        throw new Exception("Required columns not found");
            //    }
            //    bool isBuyer = false;
            //    isBuyer = string.Equals(dr["IsBuyer"].ToString(), "yes", StringComparison.InvariantCultureIgnoreCase);

            //    bool isSeller = false;
            //    isSeller = string.Equals(dr["IsSeller"].ToString(), "yes", StringComparison.InvariantCultureIgnoreCase);

            //    if (isBuyer && isSeller) return CompanyType.BuyerAndSeller;
            //    if (isSeller) return CompanyType.Seller;
            //    if (isBuyer) return CompanyType.Buyer;
            //    return CompanyType.None;

            //};

            //var mappings = new List<IPropertyMapping<Company>>();
            //mappings.Add(new ColumnToPropertyMapping<Company>("ExternalId", "CounterPartID"));
            //mappings.Add(new ColumnToPropertyMapping<Company>("TradingName", "Name"));
            //mappings.Add(new ComputedPropertyMapping<Company>("CompanyType", fnCompanyType));
            //mappings.Add(new ColumnToPropertyMapping<Company>("Phone", "Phone"));
            //mappings.Add(new ColumnToPropertyMapping<Company>("Fax", "Fax"));

            //var mapper = new DataTableMapper<Company>(mappings);
            Stopwatch sw = Stopwatch.StartNew();
            //var importer = new CounterPartyImporter();
            //var list = importer.Import("data_large.csv");
            //sw.Stop();
            //Console.WriteLine($"Mapping took {sw.ElapsedMilliseconds}");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.ExternalId}\t{item.LegalName}\t{item.TradingName}\t{item.CompanyType.ToString()}\t{item.Fax}\t{item.Phone}");
            //}
        }
    }
}
