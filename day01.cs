using System.Text.RegularExpressions;

namespace adventofcode2023
{
    public static class day01
    {
        public static int GetAnswerPart1(string filepath)
        {
            List<int> lineValues = new();
            foreach (var line in File.ReadAllLines(filepath))
            {
                var numbersInLine = Regex.Matches(line, @"[0-9]");
                lineValues.Add(int.Parse(numbersInLine.First().Value + numbersInLine.Last().Value));
            }

            return lineValues.Sum();
        }

        public static int GetAnswerPart2(string filepath)
        {
            Dictionary<string, string> numberStrings = new Dictionary<string, string>()
            {
                {"one","1"},
                {"two","2"},
                {"three","3"},
                {"four","4"},
                {"five","5"},
                {"six","6"},
                {"seven","7"},
                {"eight","8"},
                {"nine","9"},
                {"1","1"},
                {"2","2"},
                {"3","3"},
                {"4","4"},
                {"5","5"},
                {"6","6"},
                {"7","7"},
                {"8","8"},
                {"9","9"},
            };

            List<int> lineValues = new();
            Regex numbersRegex = new Regex(@"[0-9]|one|two|three|four|five|six|seven|eight|nine");
            foreach (var line in File.ReadAllLines(filepath))
            {
                int startPostition = 0;
                string firstNumber = "";
                string lastNumber = "";
                while (startPostition <= line.Length - 1)
                {
                    var foundNumber = numbersRegex.Match(line, startPostition);
                    if (foundNumber.Success && firstNumber == "")
                    {
                        firstNumber = foundNumber.Value;
                        lastNumber = foundNumber.Value;
                    }
                    else if (foundNumber.Success)
                    {
                        lastNumber = foundNumber.Value;
                    }
                    else if (!foundNumber.Success)
                    {
                        break;
                    }
                    startPostition = foundNumber.Index + 1;
                }

                lineValues.Add(int.Parse(numberStrings[firstNumber] + numberStrings[lastNumber]));
            }

            return lineValues.Sum();
        }
    }
}