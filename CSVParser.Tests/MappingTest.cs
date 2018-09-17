using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSVParser.Tests
{
    [TestFixture]
    public class PropertyMappingTest
    {
        public class testC
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }
        }

        [Test]
        public void TryMap_GivenValidValue_ShouldReturnExpectedValue()
        {



            //var pm = new PropertyMapping<testC, testC => testC.   >(c=)
        }
    }
}
