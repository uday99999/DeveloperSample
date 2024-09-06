using System;
using System.Linq;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            if (n == 0) return 1;

            int factorial = 1;
            for(int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;

        }

        public static string FormatSeparators(params string[] items) 
        {
            if (items == null || items.Length == 0)
                return string.Empty;

            if (items.Length == 1)
                return items[0];

            if (items.Length == 2)
                return string.Join(" and ", items);

            string firstSetCommaSeparated = string.Join(", ", items.Take(items.Length - 1));

            return $"{firstSetCommaSeparated} and {items.Last()}";
        }
    }
}