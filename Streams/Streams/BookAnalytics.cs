using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Streams
{
    class BookAnalytics
    {
        public void ShowMostUsedWordByPosition(List<string> list, int position)
        {
            list
                .GroupBy(g => g, StringComparer.InvariantCultureIgnoreCase)
                .OrderByDescending(g => g.Count())
                .Select(g => $"\"{g.Key}\", which was repeated {g.Count()} times")
                .ElementAt(position)
                .Log($"Most used word at position {position} is");
        }

        public void ShowMostUsedWordByLength(List<string> list, int length)
        {
            list
                .GroupBy(g => g, StringComparer.InvariantCultureIgnoreCase)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Where(key => key.Count() > length)
                .First()
                .Log($"Most used word longer than {length} characters is: ");
        }

        public void ShowWordAppearanceCount(List<string> list, string word)
        {
            list
                .Where(item => item.Equals(word, StringComparison.InvariantCultureIgnoreCase))
                .Count()
                .Log($"Word {word} appeared: ");
        }
    }
}
