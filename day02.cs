using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace adventofcode2023
{
    public class day02
    {
        public static int GetAnswerPart1(string filepath, int red, int green, int blue)
        {
            int result = 0;
            foreach (var line in File.ReadAllLines(filepath))
            {
                int gameId = GetGameFromLine(line, red, green, blue);
                result += gameId;
            }
            return result;
        }

        public static long GetAnswerPart2(string filepath)
        {
            long result = 0;
            foreach (var line in File.ReadAllLines(filepath))
            {
                int powerOfCubes = GetGameFromLinePart2(line);
                result += powerOfCubes;
            }
            return result;
        }

        static int GetGameFromLine(string line, int red, int green, int blue)
        {
            var gameSplit = line.Split(':');
            var gameId = int.Parse(gameSplit[0].Split(' ')[1].Trim());
            foreach (var set in gameSplit[1].Split(';'))
            {
                int redRunning = 0;
                int blueRunning = 0;
                int greenRunning = 0;

                foreach (var setColour in set.Split(','))
                {
                    var gameSetSplit = setColour.Trim().Split(' ');
                    switch (gameSetSplit[1])
                    {
                        case "blue":
                            blueRunning += int.Parse(gameSetSplit[0]);
                            break;
                        case "red":
                            redRunning += int.Parse(gameSetSplit[0]);
                            break;
                        case "green":
                            greenRunning += int.Parse(gameSetSplit[0]);
                            break;
                        default:
                            break;
                    }
                }
                if (greenRunning > green || redRunning > red || blueRunning > blue)
                {
                    return 0;
                }
            }
            return gameId;
        }

        static int GetGameFromLinePart2(string line)
        {
            var gameSplit = line.Split(':');
            int red = 0;
            int blue = 0;
            int green = 0;
            foreach (var set in gameSplit[1].Split(';'))
            {
                foreach (var setColour in set.Split(','))
                {
                    var gameSetSplit = setColour.Trim().Split(' ');
                    switch (gameSetSplit[1])
                    {
                        case "blue":
                            if (blue < int.Parse(gameSetSplit[0]))
                            {
                                blue = int.Parse(gameSetSplit[0]);
                            }
                            break;
                        case "red":
                            if (red < int.Parse(gameSetSplit[0]))
                            {
                                red = int.Parse(gameSetSplit[0]);
                            }
                            break;
                        case "green":
                            if (green < int.Parse(gameSetSplit[0]))
                            {
                                green = int.Parse(gameSetSplit[0]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return blue * red * green;
        }
    }
}
