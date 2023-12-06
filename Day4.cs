using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day4
    {
        public double SolvePart1(string input)
        {
            double sum = 0;
            foreach(var line in input.Split("\r\n"))
            {
                string[] boards = line.Split(':')[1].Split('|');
                string[] haveNumbers = boards[1].Replace("  "," ").Trim().Split(' ');
                string[] winningNumbers = boards[0].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x) && haveNumbers.Contains(x)).ToArray();
                sum += (winningNumbers.Length > 0)  ? Math.Pow(2, winningNumbers.Length-1) : 0;
            }
            return sum;
        }

        public long SolvePart2(string input)
        {
            Dictionary<int, int> map = new Dictionary<int, int>() {  };
            int count = 0;
            string[] lines = input.Split("\r\n");
            foreach (var line in lines)
            {
                if(!map.ContainsKey(count))
                {
                    map[count] = 1;
                }
                string[] boards = line.Split(':')[1].Split('|');
                string[] haveNumbers = boards[1].Replace("  ", " ").Trim().Split(' ');
                int winningNumbers = boards[0].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x) && haveNumbers.Contains(x)).Count();
                for (int i = 1;i <= winningNumbers && i<= lines.Length; i++)
                {
                    if(!map.ContainsKey(count+i))
                        map[count + i] = 1;
                    map[count + i] += map[count] ;
                }
                count ++;
            }
            return map.Sum((x)=>x.Value);
        }
    }
}
