using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day3
    {
        public long SolvePart1(string input)
        {
            long sum = 0;
            string[] lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                string last_chars = "";
                bool isAdjacent = false;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char character = lines[i][j];
                    if (char.IsDigit(character))
                    {
                        last_chars += character;
                        if (!isAdjacent)
                        {
                            isAdjacent = lookForAdjacent((i, j), lines) != null;
                        }
                    }
                    else
                    {
                        if (isAdjacent)
                            sum += int.Parse(last_chars);
                        last_chars = "";
                        isAdjacent = false;
                    }
                }
                if (isAdjacent)
                    sum += int.Parse(last_chars);
            }
            return sum;
        }


        public long SolvePart2(string input)
        {
            string[] lines = input.Split("\r\n");
            long sum = 0;
            Dictionary<(int, int), int> matches = new Dictionary<(int, int), int>();

            void adjacentFound((int,int)? adjacent,string last_chars)
            {
                if (adjacent.HasValue)
                {
                    if (matches.ContainsKey(adjacent.Value))
                    {
                        sum += matches[adjacent.Value] * int.Parse(last_chars);
                        matches.Remove(adjacent.Value);
                    }
                    else
                    {
                        matches.Add(adjacent.Value, int.Parse(last_chars));
                    }
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                string last_chars = "";
                (int,int)? adjacent = null;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char character = lines[i][j];
                    if (char.IsDigit(character))
                    {
                        last_chars += character;
                        if (!adjacent.HasValue)
                        {
                            adjacent = lookForAdjacent((i, j), lines, '*');
                        }
                    }
                    else
                    {
                        adjacentFound(adjacent, last_chars);

                        last_chars = "";
                        adjacent = null;
                    }
                }
                adjacentFound(adjacent, last_chars);
            }
            return sum;
        }



        private (int,int)? lookForAdjacent((int, int) pos, string[] input,char? special = null)
        {
            return IsSpecialChar((pos.Item1 - 1, pos.Item2), input,special) ??
                        IsSpecialChar((pos.Item1, pos.Item2 - 1), input,special) ??
                        IsSpecialChar((pos.Item1 + 1, pos.Item2), input,special) ??
                        IsSpecialChar((pos.Item1, pos.Item2 + 1), input, special) ??
                        IsSpecialChar((pos.Item1 - 1, pos.Item2 - 1), input,special) ??
                        IsSpecialChar((pos.Item1 + 1, pos.Item2 + 1), input,special) ??
                        IsSpecialChar((pos.Item1 - 1, pos.Item2 + 1), input,special) ??
                        IsSpecialChar((pos.Item1 + 1, pos.Item2 - 1), input, special);
        }
        private (int,int)? IsSpecialChar((int, int) pos, string[] input,char? special = null)
        {
            if (pos.Item1 > 0 && pos.Item2 > 0 && pos.Item1 < input.Length && pos.Item2 < input[pos.Item1].Length)
            {
                if (input[pos.Item1][pos.Item2] == '.' || char.IsDigit(input[pos.Item1][pos.Item2]))
                {
                    return null;
                }
                else if(special == null || input[pos.Item1][pos.Item2] == special.Value)
                {
                    return (pos.Item1,pos.Item2);
                }else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
