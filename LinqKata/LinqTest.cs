using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LinqKata
{
    [TestClass]
    public class LinqTest
    {
        private List<Dummy> values = new List<Dummy>();
        private List<string> votes = new List<string>();
        private List<string> pets = new List<string>();
        private List<int> score = new List<int>();
        private List<int> numbers = new List<int>();

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

            this.votes = new List<string>()
            {
                "Yes", "Yes", "No", "Yes", "No", "Yes", "No", "No", "No",
                "Yes", "Yes", "Yes", "No", "Yes", "No", "No", "Yes", "No",
                "Yes", "Yes", "No", "Yes", "Yes", "Yes", "No", "No", "Yes",
            };

            this.pets = new List<string>()
            {
                "Cat", "Dog", "Cat", "Chinchilla", "Rabbit", "Chinchilla", "Fish", "Fish", "Rabbit", "Cat", "Dog",
                "Fish", "Leopard", "Cat", "Fish", "Chinchilla", "Cat", "Hamster", "Hamster", "Chinchilla", "Dog",
                "Cat", "Dog", "Dog", "Fish", "Rabbit", "Chinchilla", "Hamster", "Rabbit", "Dog", "Dog", "Dog", "Cat",
                "Fish", "Fish", "Chinchilla", "Rabbit", "Dog", "Cat", "Hamster", "Rabbit", "Dog", "Dog", "Dog", "Dog"
            };

            this.score = new List<int>() { 10, 5, 0, 8, 10, 1, 4, 0, 10, 1 };

            this.numbers = Enumerable.Range(0, 100).ToList();
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

        [TestMethod]
        public void CountVotesTest()
        {
            var result = Linq.CountVotes(votes);

            CollectionAssert.AreEquivalent(new List<int> { 15, 12 }, result);
        }

        [TestMethod]
        public void CountPetsTest()
        {
            var result = Linq.CountPets(pets);

            CollectionAssert
                .AreEquivalent(new List<string>
                    {
                        "Cat:8", "Chinchilla:6", "Dog:13", "Fish:7", "Hamster:4", "Leopard:1", "Rabbit:6"
                    }
                    , result);
        }

        [TestMethod]
        public void TotalScoreTest()
        {
            var result = Linq.TotalScore(score);

            Assert.AreEqual(48, result);
        }

        [TestMethod]
        public void NumbersFromFiveToFiveTest()
        {
            var result = Linq.NumbersFromFiveToFive(numbers);

            CollectionAssert.AreEquivalent(new List<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95 }, result);
        }
    }
}
