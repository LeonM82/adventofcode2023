using System.Text.RegularExpressions;

namespace adventofcode2023
{
    public class day03
    {
        public static int GetAnswerPart1(string filepath)
        {
            List<Day03Match> specialCharacters = GetListOfMatches(filepath, @"[^A-Za-z0-9.]");
            List<Day03Match> numbers = GetListOfMatches(filepath, @"[0-9]+");

            List<Day03Match> numbersToSum = new();
            List<int> numbersToSumConverted = new();

            foreach (Day03Match specialCharacter in specialCharacters)
            {
                numbers.Where(x =>
                x.Matched == false
                && x.LineNumber >= specialCharacter.LineNumber - 1
                && x.LineNumber <= specialCharacter.LineNumber + 1
                && ShouldInclude(specialCharacter.StartIndex, x)
                ).ToList().ForEach(x =>
                {
                    numbersToSum.Add(x);
                    x.Matched = true;
                    numbersToSumConverted.Add(int.Parse(x.Value));
                });
            }

            return numbersToSumConverted.Sum();
        }

        public static int GetAnswerPart2(string filepath)
        {
            List<Day03Match> specialCharacters = GetListOfMatches(filepath, @"[*]");
            List<Day03Match> numbers = GetListOfMatches(filepath, @"[0-9]+");

            List<int> numbersToSumConverted = new();

            foreach (Day03Match specialCharacter in specialCharacters)
            {
                var numberToUse = numbers.Where(x =>
                x.Matched == false
                && x.LineNumber >= specialCharacter.LineNumber - 1
                && x.LineNumber <= specialCharacter.LineNumber + 1
                && ShouldInclude(specialCharacter.StartIndex, x)
                ).ToList();
                if (numberToUse.Count == 2)
                {
                    numbersToSumConverted.Add(int.Parse(numberToUse.First().Value) * int.Parse(numberToUse.Last().Value));
                }
            }

            return numbersToSumConverted.Sum();
        }

        static bool ShouldInclude(int index, Day03Match day03Match)
        {
            return index >= day03Match.StartIndex - 1 && index <= day03Match.EndIndex + 1;
        }

        static List<Day03Match> GetListOfMatches(string filepath, string regexPattern)
        {
            List<Day03Match> matchCollections = new();

            int lineNumber = -1;
            foreach (var line in File.ReadAllLines(filepath))
            {
                lineNumber += 1;
                foreach (Match item in Regex.Matches(line, regexPattern))
                {
                    matchCollections.Add(new Day03Match()
                    {
                        LineNumber = lineNumber,
                        Value = item.Value,
                        StartIndex = item.Index,
                        EndIndex = item.Index + item.Length - 1
                    });
                }

            }
            return matchCollections;
        }

        internal class Day03Match
        {
            public int LineNumber { get; set; }
            public string Value { get; set; } = "";
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public bool Matched { get; set; } = false;
        }
    }
}