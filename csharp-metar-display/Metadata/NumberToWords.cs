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

        /// <summary>
        /// Converts a US statute-mile visibility (e.g. 1.25, 2.5, 0.0625, 10) into words,
        /// keeping both the whole-mile part and the fractional part.
        /// </summary>
        public static string ConvertVisibilityToText(float statuteMiles)
        {
            // round to the nearest sixteenth, the smallest fraction used in US METARs
            var sixteenths = (int)Math.Round(statuteMiles * 16);
            var whole = sixteenths / 16;
            var fraction = sixteenths % 16;

            var words = whole > 0 ? ConvertToDigits(whole) : string.Empty;
            if (fraction > 0)
            {
                if (whole > 0)
                    words += "and ";
                words += ConvertFloatToText(fraction / 16f);
            }
            return words == string.Empty ? "zero " : words;
        }

        public static string ConvertFloatToText(float number)
        {
            string words = "";

            // compare in sixteenths so values like 1/16 (0.0625) match reliably
            switch ((int)Math.Round(number * 16))
            {
                case 1:
                    words += "one-sixteenth ";
                    break;
                case 2:
                    words += "one-eighth ";
                    break;
                case 3:
                    words += "three-sixteenth ";
                    break;
                case 4:
                    words += "one-quarter ";
                    break;
                case 5:
                    words += "five-sixteenth ";
                    break;
                case 6:
                    words += "three-eighth ";
                    break;
                case 8:
                    words += "one-half ";
                    break;
                case 10:
                    words += "five-eighth ";
                    break;
                case 12:
                    words += "three-quarter ";
                    break;
                case 14:
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
