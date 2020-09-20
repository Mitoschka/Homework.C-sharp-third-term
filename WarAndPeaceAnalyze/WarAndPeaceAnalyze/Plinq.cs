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
        private List<string> words = new List<string>();
        private List<int> countOfWords = new List<int>();
        private int count = 0;

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
            foreach (KeyValuePair<string, int> pair in result)
            {
                if (pair.Key.Length > 3)
                {
                    string word = pair.Key;
                    word = word.Replace(",", "");
                    words.Add(word);
                    countOfWords.Add(pair.Value);
                    i++;
                }
            }
            Console.WriteLine("   Toп 10 слов\n");
            for (int k = 1; k < 11; k++)
            {
                if (k == 10)
                {
                    Console.Write($"{k} место - ");
                }
                else
                {
                    Console.Write($"{k}  место - ");
                }
                GetMaxCount();
            }
        }

        /// <summary>
        /// Returns the most frequently repeated word from the list.
        /// </summary>
        public void GetMaxCount()
        {
            int maxValue = countOfWords[0];

            for (int i = 1; i < countOfWords.Count; i++)
            {
                if (maxValue < countOfWords[i])
                {
                    maxValue = countOfWords[i];
                    count = i;
                }
            }
            Console.WriteLine($"({words[count]} = {countOfWords[count]})");
            countOfWords.RemoveAt(count);
            words.RemoveAt(count);
        }
    }
}
