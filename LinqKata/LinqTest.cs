using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqKata
{
    [TestClass]
    public class LinqTest
    {
        private List<Dummy> values = new List<Dummy>();

        [TestInitialize]
        public void Initialize()
        {
            this.values = new List<Dummy>()
            {
                new Dummy () { Id = 1, Name = "Name", Names = new List<string>() { "Names1", "Names2" } },
                new Dummy () { Id = 13, Name = "Name3", Names = new List<string>() { "Names1", "Names2" } },
                new Dummy () { Id = 14, Name = "Name4", Names = new List<string>() { "Names14", "Nam4es24" } },
                new Dummy () { Id = 15, Name = "Name5", Names = new List<string>() { "Na12mes15", "Na3mes25" } },
            };
        }

        [TestMethod]
        public void GetNamesTest()
        {
            var result = Linq.GetNames(values);

            Assert.IsTrue(values.Count == result.Count);
        }

        [TestMethod]
        public void GetNumbersOfNamesTest()
        {
            var result = Linq.GetNumbersOfNames(values);

            CollectionAssert.AreEquivalent(new List<string> { "1", "2", "1", "2", "14", "424", "1215", "325" }, result);
        }
    }
}
