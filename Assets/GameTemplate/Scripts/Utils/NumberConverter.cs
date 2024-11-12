using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GameTemplate.Utils
{
    public static class NumberHelper
    {
        public static Dictionary<int, string> numExponentialTable;

        static string periodString = ".";
        static string whiteSpaceString = " ";
        static string dashString = "-";
        static string zeroString = "0";
        static StringBuilder builder = new StringBuilder();
        static string[] optimizedNumberToStringArray;

        /// <summary>
        /// Converts a float to string using the scientific connotation (K,M,B,T,aa,etc.)
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>The converted string</returns>
        public static string ToStringScientific(this float number)
        {
            InitializeToString();

            return FormatNumber(number);
        }

        /// <summary>
        /// Converts a float to string using the scientific connotation (K,M,B,T,aa,etc.)
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>The converted string</returns>
        public static string FormatNumber(float num)
        {
            int maxAllowed = 3;
            int decimals = 0;

            numExponentialTable ??= new Dictionary<int, string>
            {
                { 3, "K" },
                { 6, "M" },
                { 9, "B" },
                { 12, "T" },
                { 15, "aa" },
                { 18, "bb" },
                { 21, "cc" },
                { 24, "dd" },
                { 27, "ee" },
                { 30, "ff" },
                { 33, "gg" },
                { 36, "hh" },
                { 39, "ii" },
                { 42, "jj" },
                { 45, "kk" },
                { 48, "ll" },
                { 51, "mm" },
                { 54, "nn" },
                { 57, "oo" },
                { 60, "pp" },
                { 63, "qq" },
                { 66, "rr" },
                { 69, "ss" },
                { 72, "tt" }
            };

            builder.Length = 0;
            bool negative = num < 0;
            float n = Mathf.Abs(Mathf.Floor(num));

            int mult = (int)Mathf.Floor(Mathf.Log10(n));
            if (mult < maxAllowed)
            {
                if (decimals == 0)
                    return GetNumberOptimized((int)n);
                else
                {
                    int aux = decimals * 10;
                    return builder.Append(GetNumberOptimized((int)n)).Append(periodString)
                        .Append(GetNumberOptimized(((int)(num * aux)) % aux)).ToString();
                }
            }
            else
            {
                mult = Mathf.FloorToInt(mult / maxAllowed) * maxAllowed;
                int left = Mathf.FloorToInt(n / Mathf.Pow(10, mult));
                int right = Mathf.FloorToInt(100 * (n / Mathf.Pow(10, mult) - left));

                if (Mathf.Floor(Mathf.Log10(left) + 1) >= 4)
                    return builder.Append((negative ? dashString : string.Empty)).Append(GetNumberOptimized(left))
                        .Append(whiteSpaceString).Append(numExponentialTable[mult]).ToString();
                else
                    return builder.Append((negative ? dashString : string.Empty)).Append(GetNumberOptimized(left))
                        .Append(periodString).Append((right < 10 ? zeroString : string.Empty))
                        .Append(right + whiteSpaceString + numExponentialTable[mult]).ToString();
            }
        }

        private static void InitializeToString()
        {
            if (optimizedNumberToStringArray == null)
            {
                optimizedNumberToStringArray = new string[1000];
                for (int i = 0; i < optimizedNumberToStringArray.Length; i++)
                {
                    optimizedNumberToStringArray[i] = i.ToString();
                }
            }
        }

        private static string GetNumberOptimized(int number)
        {
            if (number >= 0 && number < optimizedNumberToStringArray.Length)
                return optimizedNumberToStringArray[number];

            return string.Empty;
        }
    }
}