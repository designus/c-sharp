using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"A:\Learning\C#\Streams\Streams\originofspecies.txt";
            string outputPath = @"A:\Learning\C#\Streams\Streams\output.txt";

            ReadBookSync(inputPath);
            ReadAndWriteSync(inputPath, outputPath);
        }

        static void ReadBookSync(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                List<string> wordList = GetListOfWordsFromText(text);
                BookAnalytics bookAnalytics = new BookAnalytics();

                bookAnalytics.ShowMostUsedWordByPosition(wordList, 12);
                bookAnalytics.ShowMostUsedWordByLength(wordList, 5);
                bookAnalytics.ShowWordAppearanceCount(wordList, "varieties");
            }
        }

        static void ReadAndWriteSync(string inputPath, string outputPath)
        {
            using (StreamReader reader = new StreamReader(inputPath))
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                string line = "";

                while ((line = reader.ReadLine()) != null)
                {
                    List<string> input = GetListOfWordsFromText(line)
                        .Where(word => word.Count() > 10)
                        .ToList();

                    foreach (var item in input)
                    {
                        writer.WriteLine(item);
                    }
                }

                Console.WriteLine("Finished...");

            }
        }

        static List<string> GetListOfWordsFromText(string text)
        {
            MatchCollection matchList = Regex.Matches(text, @"[a-zA-Z0-9]+", RegexOptions.IgnoreCase);
            return matchList
                .Select(match => match.Value)
                .ToList();
        }
    }

}
