using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqKata
{
    public static class Linq
    {
        public static List<string> GetNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach(var value in values)
            {
                filtered.Add(value.Name);
            }

            return filtered;
        }

        public static List<string> FilterByMinId(List<Dummy> values, int min)
        {
            List<string> filtered = new List<string>();

            foreach(var value in values)
            {
                if(value.Id >= min)
                    filtered.Add(value.Name);
            }

            return filtered;
        }

        public static List<Dummy> Slice(List<Dummy> values, int start, int end)
        {
            List<Dummy> filtered = new List<Dummy>();

            for(int i = start; i < values.Count || i <= end; i++)
            {
                filtered.Add(values[i]);
            }

            return filtered;
        }

        public static List<string> AllNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach(var value in values)
            {
                filtered.AddRange(value.Names);
            }

            return filtered;
        }

        public static List<string> AllDistinctNames(List<Dummy> values)
        {
            List<string> filtered = new List<string>();

            foreach(var value in values)
            {
                foreach(string name in value.Names)
                {
                    if(!filtered.Contains(name)) filtered.Add(name);
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
        /// Pets = new List<string>() { "Dog", "Cat", "Rabbit", "Dog", "Dog", "Cat" };
        /// 
        /// Lista con el nombre del animal seguido de : y el número de veces que se repite (SIN ESPACIOS)
        /// Ordenada ascendente por el animal
        /// Debería devolver -> new List<string>() {"cat:2", "dog:3", "rabbit:1"};
        /// </summary>
        /// <param name="pets"></param>
        /// <returns></returns>
        public static List<string> CountPets(List<string> pets)
        {
            return pets.GroupBy(n => n.ToLower())
                .Select(n => new { n.Key, Count = n.Count() })
                .OrderBy(n => n.Key)
                .Select(n => $"{n.Key}:{n.Count}")
                .ToList();
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
            return score.OrderBy(s => s).Skip(3).Sum();
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
            return numbers.Where((x, i) => i % 5 == 0).ToList();
        }

        /// <summary>
        /// expand = new List<string>() { "A5", "B10", "C", "D2" };
        /// 
        /// Cadena con la letra repetida tantas veces como diga su número, sin número significa una sola vez
        /// Devolver un string AAAAABBBBBBBBBBCDD
        /// </summary>
        /// <param name="expand"></param>
        /// <returns></returns>
        public static string ExpandLetters(string expand)
        {
            Regex r = new Regex(@"(\w)(\d*)");

            return r.Matches(expand)
                .Select(m => new { Letter = m.Groups[1].Value, Count = m.Groups[2].Value })
                .Select(g => new { Letter = g.Letter[0], Count = string.IsNullOrEmpty(g.Count) ? 1 : int.Parse(g.Count) })
                .Aggregate(new StringBuilder(), (seed, next) => seed.Append(next.Letter, next.Count))
                .ToString();
        }
    }

    public class Dummy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Names { get; set; }
    }
}
