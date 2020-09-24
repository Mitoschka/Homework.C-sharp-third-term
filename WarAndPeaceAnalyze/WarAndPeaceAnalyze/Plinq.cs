using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Global namespace.
/// </summary>
namespace WarAndPeaceAnalyze
{
    /// <summary>
    /// Calculating statistics for txt files using PLINQ.
    /// </summary>
    class Plinq
    {
        private readonly List<string> words = new List<string>();

        /// <summary>
        /// Collects data for further analysis.
        /// </summary>
        public Plinq()
        {
            string[] textMass;
            string text = File.ReadAllText(@"WarAndPeace.txt");
            int count = File.ReadAllLines(@"WarAndPeace.txt").Length;
            textMass = text.Split(' ');
            Console.WriteLine("Количество слов:");
            Console.WriteLine(textMass.Length + "\n");
            Console.WriteLine("Количество строк:");
            Console.WriteLine(count + "\n");
            var result = (textMass.AsParallel()
             .Select(str => new { Name = str, Count = textMass.Count(s => s == str) })
             .Where(obj => obj.Count > 1)
             .Distinct()
             .ToDictionary(obj => obj.Name, obj => obj.Count));
            int i = 0;
            foreach (var pair in result.Where(pair => pair.Key.Length > 3))
            {
                string word = pair.Key;
                word = word.Replace(",", "");
                words.Add(word);
                i++;
            }

            Console.WriteLine("\n   Toп 10 слов\n");
            string[] tenMostCommon = FindTenMostCommon();
            int place = 1;
            foreach (var word in tenMostCommon)
            {
                Console.WriteLine($"{place} место - {word}");
                place++;
            }
        }

        /// <summary>
        /// Returns the most frequently repeated word from the list.
        /// </summary>
        private string[] FindTenMostCommon()
        {
            var frequencyOrder = from word in words
                                 where word.Length > 3
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;
            string[] commonWords = (frequencyOrder.AsParallel().Take(10)).ToArray();
            return commonWords;
        }

    }
}
