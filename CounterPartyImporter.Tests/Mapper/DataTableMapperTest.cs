using DataImporter.Mapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataImporter.Tests.Mapper
{
    [TestFixture]
    public class DataTableMapperTest
    {
        public DataTable dataTable { get; private set; }

        class TestC
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("testName");
            dataTable.Columns.Add("testId");
            var dr = dataTable.NewRow();
            dr["testName"] = "X001";
            dr["testId"] = 1;
            dataTable.Rows.Add(dr);
            dr = dataTable.NewRow();
            dr["testName"] = "X002";
            dr["testId"] = 2;
            dataTable.Rows.Add(dr);
        }

        [Test]
        public void Map_GivenMatchingColumns_ShouldReturnExpectedList()
        {
            var mappings = new List<IPropertyMapping<TestC>>();
            mappings.Add(new ColumnToPropertyMapping<TestC>("Name", "testName"));
            mappings.Add(new ColumnToPropertyMapping<TestC>("Id", "testId"));

            var mapper = new DataTableMapper<TestC>(mappings);
            var result = mapper.Map(dataTable).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("X001", result[0].Name);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("X002", result[1].Name);
            Assert.AreEqual(2, result[1].Id);
        }

        [Test]
        public void Map_GivenUnMatchingColumns_ShouldThrowMappingException()
        {
            var mappings = new List<IPropertyMapping<TestC>>();
            mappings.Add(new ColumnToPropertyMapping<TestC>("Name", "testNameX"));

            var mapper = new DataTableMapper<TestC>(mappings);
            Assert.Throws<MappingException>(() => mapper.Map(dataTable));  
        }

        [Test]
        public void ValidateMappingProperties_GivenMatchingColumns_ShouldReturnTrue()
        {
            var mappings = new List<IPropertyMapping<TestC>>();
            mappings.Add(new ColumnToPropertyMapping<TestC>("Name", "testName"));

            var mapper = new DataTableMapper<TestC>(mappings);
            Assert.IsTrue(mapper.ValidateMappingProperties());
        }

        [Test]
        public void ValidateMappingProperties_GivenUnMatchingColumns_ShouldReturnFalse()
        {
            var mappings = new List<IPropertyMapping<TestC>>();
            mappings.Add(new ColumnToPropertyMapping<TestC>("Namex", "testName"));

            var mapper = new DataTableMapper<TestC>(mappings);
            Assert.IsFalse(mapper.ValidateMappingProperties());
        }
    }
}
