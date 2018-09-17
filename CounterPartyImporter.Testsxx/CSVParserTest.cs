using CounterPartyImporter.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace CounterPartyImporter.Tests
{
    [TestFixture]
    public class CSVParserTest
    {
        MockFileSystem fileSystem;
        public void Setup()
        {
            fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    { @"C:\counterParty01.csv", new MockFileData(@"CounterPartID,Name,IsBuyer,IsSeller,Phone,Fax
B10001,Test Company 1,Yes,No,3165656667,319889808
B10002,Test Company 2,Yes,No,3165656667,319889808
B10003,Test Company 3,Yes,Yes,3165656667,319889808
B10004,Test Company 4,Yes,Yes,3165656667,319889808
B10018,Test Company 18,No,Yes,3165656667,319889808") },
                    { @"C:\counterParty01.csv", new MockFileData(@"CounterPartID,Name,IsBuyer,IsSeller,Phone,Fax") }
                });

        }
        public void ConvertToDataTable_GivenFileHavingData_ReturnDataTable()
        {
            var mockFileSystem = new MockFileSystem();
            CSVParser csvParser = new CSVParser(mockFileSystem);
            var dt = csvParser.ConvertToDataTable(@"C:\counterParty01.csv");
            Assert.AreEqual(5, dt.Rows.Count);
        }
    }
}
