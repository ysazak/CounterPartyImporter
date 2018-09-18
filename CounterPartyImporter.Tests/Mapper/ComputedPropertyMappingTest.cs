using DataImporter.Mapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataImporter.Tests.Mapper
{
    [TestFixture]
    public class ComputedPropertyMappingTest
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
            dr = dataTable.NewRow();
            dr["testName"] = "X002";
            dr["testId"] = 2;
            dataTable.Rows.Add(dr);
        }


        class TestC
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        Func<DataRow, object> validFn = (dr) => { return $"{dr["testName"].ToString()}_{dr["testId"]}"; };

        Func<DataRow, object> invalidFn = (dr) => { return $"{dr["testName"].ToString()}_{dr["Id"]}"; };

        [Test]
        public void TrySetValue_GivenValidFuncAndPropertyNames_ShouldReturnExpectedValues()
        {

            var mapping = new ComputedPropertyMapping<TestC>("Name", validFn);
            var prop = typeof(TestC).GetProperty("Name");
            var instance = new TestC();
            Assert.IsTrue(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
            Assert.AreEqual("X001_1", instance.Name);
        }

        [Test]
        public void TrySetValue_GivenFuncHasInvalidColumn_ShouldReturnFalse()
        {

            var mapping = new ComputedPropertyMapping<TestC>("Name", invalidFn);
            var prop = typeof(TestC).GetProperty("Name");
            var instance = new TestC();
            Assert.IsFalse(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
        }

        [Test]
        public void TrySetValue_GivenUnmatchingProperty_ShouldReturnFalse()
        {

            var mapping = new ComputedPropertyMapping<TestC>("_Name", validFn);
            var prop = typeof(TestC).GetProperty("_Name");
            var instance = new TestC();
            Assert.IsFalse(mapping.TrySetValue(instance, prop, dataTable.Rows[0]));
        }
    }
}
