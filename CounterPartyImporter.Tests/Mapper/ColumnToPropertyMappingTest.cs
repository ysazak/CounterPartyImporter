using DataImporter.Mapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataImporter.Tests.Mapper
{
    [TestFixture]
    public class ColumnToPropertyMappingTest
    {
        public DataTable dataTable { get; private set; }

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
        }


        class TestC
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        [Test]
        public void TrySetValue_GivenValidColumnAndPropertyNames_ShouldReturnExpectedValues()
        {

            var mapping = new ColumnToPropertyMapping<TestC>("Name", "testName");
            var prop = typeof(TestC).GetProperty("Name");
            var instance = new TestC();
            Assert.IsTrue(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
            Assert.AreEqual("X001", instance.Name);
        }

        [Test]
        public void TrySetValue_GivenInvalidColumn_ShouldReturnFalse()
        {

            var mapping = new ColumnToPropertyMapping<TestC>("Name", "_Name");
            var prop = typeof(TestC).GetProperty("Name");
            var instance = new TestC();
            Assert.IsFalse(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
        }

        [Test]
        public void TrySetValue_GivenUnmatchingProperty_ShouldReturnFalse()
        {

            var mapping = new ColumnToPropertyMapping<TestC>("_Name", "testName");
            var prop = typeof(TestC).GetProperty("_Name");
            var instance = new TestC();
            Assert.IsFalse(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
        }
    }
}
