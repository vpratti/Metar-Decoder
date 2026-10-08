using System;

namespace csharp_metar_display.metadata
{
    public static class NumberToWords
    {
        public static string ConvertToDigits(int number)
        {
            string words = "";

            for (int i = 0; i < Math.Abs(number).ToString().Length; i++)
            {
                int digit = int.Parse(Math.Abs(number).ToString().Substring(i, 1));
                switch (digit)
                {
                    case 0:
                        words += "zero ";
                        break;
                    case 1:
                        words += "one ";
                        break;
                    case 2:
                        words += "two ";
                        break;
                    case 3:
                        words += "three ";
                        break;
                    case 4:
                        words += "four ";
                        break;
                    case 5:
                        words += "five ";
                        break;
                    case 6:
                        words += "six ";
                        break;
                    case 7:
                        words += "seven ";
                        break;
                    case 8:
                        words += "eight ";
                        break;
                    case 9:
                        words += "nine ";
                        break;
                }
            }

            if (number < 0)
                return "minus " + words;
            else
                return words;
        }

        public static string ConvertFloatToText(float number)
        {
            string words = "";

            switch (number)
            {
                case 0.25f:
                    words += "one-quarter ";
                    break;
                case 0.50f:
                    words += "one-half ";
                    break;
                case 0.75f:
                    words += "three-quarter ";
                    break;
                case 0.063f:
                    words += "one-sixteenth ";
                    break;
                case 0.125f:
                    words += "one-eighth ";
                    break;
                case 0.188f:
                    words += "three-sixteenth ";
                    break;
                case 0.313f:
                    words += "five-sixteenth ";
                    break;
                case 0.375f:
                    words += "three-eighth ";
                    break;
                case 0.625f:
                    words += "five-eighth ";
                    break;
                case 0.875f:
                    words += "seven-eighth ";
                    break;
            }
            return words;
        }

        public static string ConvertToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
