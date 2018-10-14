using System;
using System.Collections.Generic;
using System.Text;

namespace Streams
{
    public static class ExtensionMethods
    {
        public static void Log<T>(this IEnumerable<T> list, string message)
        {
            Console.WriteLine(message);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------");
        }

        public static void Log(this int value, string message)
        {
            Console.WriteLine($"{message} {value}");
        }

        public static void Log(this string value, string message)
        {
            Console.WriteLine($"{message} {value}");
        }
    }
}
