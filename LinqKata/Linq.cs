using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqKata
{
    public static class Linq
    {
        //private List<LinqTest> values = new List<LinqTest>();

        //public Linq()
        //{
        //    this.values = new List<LinqTest>()
        //    {
        //        new LinqTest () { Id = 1, Name = "Name", Names = new List<string>() { "Names1", "Names2" } },
        //        new LinqTest () { Id = 11, Name = "Name1", Names = new List<string>() { "Names11", "Names21" } },
        //        new LinqTest () { Id = 12, Name = "Name2", Names = new List<string>() { "Names12", "Names22" } },
        //        new LinqTest () { Id = 13, Name = "Name3", Names = new List<string>() { "Names1", "Names2" } },
        //        new LinqTest () { Id = 14, Name = "Name4", Names = new List<string>() { "Names14", "Names24" } },
        //        new LinqTest () { Id = 15, Name = "Name5", Names = new List<string>() { "N1ames15", "Nam32es25" } },
        //    };
        //}

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

        public static List<string> GetNumbersOfNames(List<Dummy> values)
        {
            return values.SelectMany(v => v.Names.Select(n => new string(n.Where(char.IsDigit).ToArray()))).ToList();
        }
    }

    public class Dummy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Names { get; set; }
    }
}
