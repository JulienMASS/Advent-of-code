using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day2
    {
        public int SolvePart1(string input)
        {
            int sum = 0;
            string[] lines = input.Split("\r\n");

            for (int i = 0; i<lines.Length;i++)
            {

                Dictionary<string, int> balls = new Dictionary<string, int>()
                {
                    {"red",12},
                    {"green",13},
                    {"blue",14}
                };
                bool valid = true;
                string games = lines[i].Split(":")[1];
                foreach (string set in games.Split(";"))
                {
                    foreach(string pick in set.Split(','))
                    {
                        string[] record = pick.Trim().Split(' ');
                        string color = record[1];
                        valid &= balls[color] >= int.Parse(record[0]);
                        if (!valid)
                            break;
                    }
                    if(!valid)
                        break;
                }
                if(valid)
                    sum+=i+1;
            }
            return sum;
        }

        public int SolvePart2(string input)
        {
            int sum = 0;
            string[] lines = input.Split("\r\n");
            for (int i = 0; i< lines.Length;i++)
            {
                string games = lines[i].Split(":")[1];
                Dictionary<string, int> minimum = new Dictionary<string, int>()
                {
                    {"red",0},
                    {"green",0},
                    {"blue",0}
                };
                foreach (string set in games.Split(";"))
                {
                    foreach(string pick in set.Split(','))
                    {
                        string[] record = pick.Trim().Split(' ');
                        string color = record[1];
                        minimum[color] = Math.Max(minimum[color], int.Parse(record[0]));
                    }
                }
                sum+= minimum["red"]*minimum["blue"]*minimum["green"];
            }
            return sum;
        }
    }
}
