using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqKata
{
    public static class Linq
    {
        public static List<string> GetNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach (var value in values)
            {
                filtered.Add(value.Name);
            }

            return filtered;
        }

        public static List<string> FilterByMinId(List<Dummy> values, int min)
        {
            List<string> filtered = new List<string>();

            foreach (var value in values)
            {
                if (value.Id >= min)
                    filtered.Add(value.Name);
            }

            return filtered;
        }

        public static List<Dummy> Slice(List<Dummy> values, int start, int end)
        {
            List<Dummy> filtered = new List<Dummy>();

            for (int i = start; i < values.Count || i <= end; i++)
            {
                filtered.Add(values[i]);
            }

            return filtered;
        }

        public static List<string> AllNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach (var value in values)
            {
                filtered.AddRange(value.Names);
            }

            return filtered;
        }

        public static List<string> AllDistinctNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach (var value in values)
            {
                foreach (string name in value.Names)
                {
                    if (!filtered.Contains(name)) filtered.Add(name);
                }
            }

            return filtered;
        }

        /// <summary>
        /// Names = new List<string>() { "Names1", "Names2" } ;
        /// Names = new List<string>() { "Names1", "Names2" } ;
        /// 
        /// Debería devolver -> new List<string>() { "1", "2", "1", "2" } ;
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<string> GetNumbersOfNames(List<Dummy> values)
        {
            return null;
        }

        /// <summary>
        /// Votes = new List<string>() { "Yes", "No", "Yes", "No", "No" }
        /// 
        /// Lista con el número de "Yes" y el número de "No"
        /// Debería devolver -> new List<int>() {2, 3};
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<int> CountVotes(List<string> votes)
        {
            return null;
        }

        /// <summary>
        /// Pets = new List<string>() { "Dog", "Cat", "Rabbit", "Dog", "Dog", "Cat" };
        /// 
        /// Lista con el nombre del animal seguido de : y el número de veces que se repite (SIN ESPACIOS)
        /// Ordenada ascendente por el animal
        /// Debería devolver -> new List<string>() {"Cat:2", "Dog:3", "Rabbit:1"};
        /// </summary>
        /// <param name="pets"></param>
        /// <returns></returns>
        public static List<string> CountPets(List<string> pets)
        {
            return null;
        }

        /// <summary>
        /// score = new List<int>() {1, 10, 2, 4, 2, 0, 1, 0}
        /// 
        /// Suma de las puntuaciones exceptuando las tres puntuaciones más bajas
        /// Debería devolver: 19
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static int TotalScore(List<int> score)
        {
            return 0;
        }

        /// <summary>
        /// numbers = new List<int>() {0, 1, 2, 3, .., 100}
        /// 
        /// Lista de número cogiendo las posiciones de 5 en 5
        /// Debería devolver -> new List<int>() {0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95}
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static List<int> NumbersFromFiveToFive(List<int> numbers)
        {
            return null;
        }

        public static string ExpandLetters(List<string> expand)
        {
            return null;
        }
    }

    public class Dummy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Names { get; set; }
    }
}
