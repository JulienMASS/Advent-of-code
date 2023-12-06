using SpanExtensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day6
    {
        public long SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var timedatas = lines[0][10..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            var recordDatas= lines[1][10..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();

            return Compute(recordDatas, timedatas);
        }
        public long SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var timedatas = new long[] { long.Parse(lines[0][10..].Trim().Replace(" ", ""))};
            var recordDatas = new long[] { long.Parse(lines[1][10..].Trim().Replace(" ", ""))};

            return Compute(recordDatas, timedatas);
        }

        public long Compute(long[] recordDatas, long[] timedatas)
        {
            long ways = 1;

            for (int i = 0; i < recordDatas.Length; i++)
            {

                long hold = (int)timedatas[i] / 2;
                long distance;
                long record = recordDatas[i];
                long time = timedatas[i];
                long counter = 0;
                do
                {
                    distance = (time - hold) * hold;
                    if (distance > record)
                        counter++;
                    hold--;
                } while (distance > record && hold >= 0);
                long beated = (time % 2 != 0) ? (counter * 2) : ((counter - 1) * 2) + 1;
                ways *= beated;
            }

            return ways;
        }


    }
}
