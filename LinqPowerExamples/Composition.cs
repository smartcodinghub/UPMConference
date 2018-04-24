using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqPowerExamples
{
    public static class Composition
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int index, int count)
        {
            return source.Skip((index - 1) * count).Take(count);
        }

        public static void ComposeLinq()
        {
            int valoration = 80;
            Type type = Type.CONSTRUCTION | Type.GAMES;

            string[] games = { "SC II|100", "AC II: Brotherhood|50", "Warcraft III|80" };
            string[] puzzles = { "NYC|80", "California|80", "Valladolid|100" };
            string[] constructions = { "Lego|100", "Geomag|80" };

            IEnumerable<string> collection = Enumerable.Empty<string>();

            if(type.HasFlag(Type.GAMES))
            {
                collection = collection.Concat(games);
            }

            if(type.HasFlag(Type.PUZZLES))
            {
                collection = collection.Concat(puzzles);
            }

            if(type.HasFlag(Type.CONSTRUCTION))
            {
                collection = collection.Concat(constructions);
            }

            collection = collection.Select(s => s.Split("|"))
                .Select(splitted => new { Name = splitted[0], Valoration = int.Parse(splitted[1]) })
                .Where(v => v.Valoration >= valoration)
                .OrderBy(v => v.Valoration)
                .ThenBy(v => v.Name)
                .Select(v => $"{v.Name} || {v.Valoration}");

            Console.WriteLine(string.Join(Environment.NewLine, collection));
        }
    }

    [Flags]
    public enum Type
    {
        GAMES, PUZZLES, CONSTRUCTION
    }
}
