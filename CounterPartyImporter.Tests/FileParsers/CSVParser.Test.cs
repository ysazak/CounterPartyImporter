using DataImporter.FileParsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace DataImporter.Tests.FileParsers
{
    [TestFixture]
    public class CSVParserTest
    {
        MockFileSystem mockFileSystem;

        [SetUp]
        public void Setup()
        {
            mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"C:\counterParty01.csv", new MockFileData(@"CounterPartID,Name,IsBuyer,IsSeller,Phone,Fax
B10001,Test Company 1,Yes,No,3165656667,319889808
B10002,Test Company 2,Yes,No,3165656667,319889808
B10003,Test Company 3,Yes,Yes,3165656667,319889808
B10004,Test Company 4,Yes,Yes,3165656667,319889808
B10018,Test Company 18,No,Yes,3165656667,319889808") },
                    { @"C:\counterParty02.csv", new MockFileData(@"CounterPartID,Name,IsBuyer,IsSeller,Phone,Fax") }
                });

        }

        [Test]
        public void ConvertToDataTable_GivenFileHavingValidData_ReturnExpectedDataTable()
        {
            CSVParser csvParser = new CSVParser(mockFileSystem);
            var dt = csvParser.ConvertToDataTable(@"C:\counterParty01.csv");
            Assert.AreEqual(5, dt.Rows.Count);
        }

        [Test]
        public void ConvertToDataTable_GivenFileHavingNoData_ReturnEmptyDataTable()
        {
            CSVParser csvParser = new CSVParser(mockFileSystem);
            var dt = csvParser.ConvertToDataTable(@"C:\counterParty02.csv");
            Assert.AreEqual(0, dt.Rows.Count);
        }
    }
}
