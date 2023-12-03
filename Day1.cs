using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day1
    {
        public int SolvePart1(string input)
        {
            int sum = 0;
            foreach (string line in input.Split("\r\n"))
            {
                char[] numbers = new char[2];
                for (int i = 0; i < line.Length; i++)
                {
                    if (numbers[0] == default && char.IsDigit(line[i]))
                        numbers[0] = line[i];
                    if (numbers[1] == default && char.IsDigit(line[line.Length - 1 - i]))
                        numbers[1] = line[line.Length - 1 - i];
                    if (numbers[0] != default && numbers[1] != default)
                        break;
                }
                sum += int.Parse(new string(numbers));
            }
            return sum;
        }

        public int SolvePart2(string input)
        {
            int sum = 0;
            Dictionary<string, char> stringDigits = new Dictionary<string, char>()
            {
                {"one",'1'},
                {"two",'2'},
                {"three",'3'},
                {"four",'4'},
                {"five",'5'},
                {"six",'6'},
                {"seven",'7'},
                {"eight",'8'},
                {"nine",'9'},
            };
            foreach (string line in input.Split("\r\n"))
            {
                char[] numbers = new char[2];
                for (int i = 0; i < line.Length; i++)
                {
                    if (numbers[0] == default) {
                        if(char.IsDigit(line[i]))
                            numbers[0] = line[i];
                        else
                        {
                            for (int j = 3; j <= 5; j++)
                            {
                                string nextChar = line[i..(Math.Min(i + j, line.Length - 1))];
                                if (stringDigits.ContainsKey(nextChar))
                                {
                                    numbers[0] = stringDigits[nextChar];
                                    break;
                                }
                            }
                        }

                    }
                    if (numbers[1] == default)
                    {
                        if (char.IsDigit(line[line.Length - 1 - i]))
                            numbers[1] = line[line.Length - 1 - i];
                        else
                        {
                            for(int j = 3; j <= 5; j++)
                            {
                                string nextChar = line[(Math.Max(0, line.Length - i - j))..(Math.Max(0, line.Length - i))];
                                if (stringDigits.ContainsKey(nextChar))
                                {
                                    numbers[1] = stringDigits[nextChar];
                                    break;
                                }
                            }
                        }

                    }
                    if (numbers[0] != default && numbers[1] != default)
                        break;
                }
                sum += int.Parse(new string(numbers));
            }
            return sum;
        }
    }
}
