using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Streams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string inputPath = @"A:\Learning\C#\Streams\Streams\originofspecies.txt";
            string outputPath = @"A:\Learning\C#\Streams\Streams\output.txt";

            ReadBookSync(inputPath);
            ReadAndWriteSync(inputPath, outputPath);
            await DownloadCatImage();

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
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

        static void ReadBookAsync(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {

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

        static async Task DownloadCatImage()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.thecatapi.com/v1/images/search?format=src&size=full";

                //using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                //using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                using (Stream streamToReadFrom = await client.GetStreamAsync(url))
                {
                    string fileToWriteTo = @"A:\Learning\C#\Streams\Streams\cat.jpg";
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }
            }
                

        }
    }

}
