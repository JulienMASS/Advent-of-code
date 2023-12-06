using SpanExtensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_code_2023
{
    internal class Day5
    {
        List<long> seeds;
        List<List<Map>> maps;

        private void parseMaps(ReadOnlySpan<char> spanInput)
        {
            maps = new List<List<Map>>();
            int currentIndex = spanInput.IndexOf(Environment.NewLine + Environment.NewLine);

            while (currentIndex < spanInput.Length && spanInput.Skip(currentIndex).IndexOf(':') != -1)
            {
                currentIndex = spanInput.Skip(currentIndex).IndexOf(':') + currentIndex;
                List<Map> currentMap = new List<Map>();
                int lastIndexOf = spanInput.Skip(currentIndex).IndexOf(Environment.NewLine + Environment.NewLine);
                if (lastIndexOf == -1)
                    lastIndexOf = spanInput.Length;
                else
                    lastIndexOf += currentIndex;
                var mapLines = spanInput[(currentIndex + 1)..lastIndexOf].Trim();
                foreach (var line in mapLines.Trim().EnumerateLines())
                {
                    currentMap.Add(Map.Parse(line));
                }
                maps.Add(currentMap);
                currentIndex = lastIndexOf + 8;
            }
        }

        public long SolvePart1(string input)
        {
            SearchValues<char> searchValues = SearchValues.Create(":");
            var spanInput = input.AsSpan();
            var t = spanInput[(spanInput.IndexOf(':')+1)..(spanInput.IndexOf(Environment.NewLine+ Environment.NewLine))];
            var x = SpanExtensions.SpanExtensions.Split(t,' ',StringSplitOptions.RemoveEmptyEntries);
            seeds = new List<long>();
            while (x.MoveNext())
            {
               seeds.Add(long.Parse(x.Current));
            }
            parseMaps(spanInput);
            return Solve();
        }

        private long Solve()
        {
            var result1 = long.MaxValue;
            foreach (var seed in seeds)
            {
                var value = seed;
                foreach (var map in maps)
                {
                    foreach (var item in map)
                    {
                        if (value >= item.from && value <= item.to)
                        {
                            value += item.adjust;
                            break;
                        }
                    }
                }
                result1 = Math.Min(result1, value);
            }
            return result1;
        }

        private long Solve(List<(long from,long to)> seedsRange)
        {
            foreach (var map in maps)
            {
                var orderedmap = map.OrderBy(x => x.from).ToList();

                var newranges = new List<(long from, long to)>();
                foreach (var r in seedsRange)
                {
                    var range = r;
                    foreach (var mapping in orderedmap)
                    {
                        if (range.from < mapping.from)
                        {
                            newranges.Add((range.from, Math.Min(range.to, mapping.from - 1)));
                            range.from = mapping.from;
                            if (range.from > range.to)
                                break;
                        }

                        if (range.from <= mapping.to)
                        {
                            newranges.Add((range.from + mapping.adjust, Math.Min(range.to, mapping.to) + mapping.adjust));
                            range.from = mapping.to + 1;
                            if (range.from > range.to)
                                break;
                        }
                    }
                    if (range.from <= range.to)
                        newranges.Add(range);
                }
                seedsRange = newranges;
            }
            var result2 = seedsRange.Min(r => r.from);
            return result2;
        }

        public long SolvePart2(string input)
        {
            SearchValues<char> searchValues = SearchValues.Create(":");
            var spanInput = input.AsSpan();
            var t = spanInput[(spanInput.IndexOf(':') + 1)..(spanInput.IndexOf(Environment.NewLine + Environment.NewLine))];
            var x = SpanExtensions.SpanExtensions.Split(t, ' ', StringSplitOptions.RemoveEmptyEntries);
            seeds = new List<long>();
            List<(long,long)> ranges = new List<(long,long)>();
            long start = -1;

            List<(long,long)> splitt((long,long) initial)
            {
                List<(long,long)> res = new List<(long, long)>();
                if(ranges.Count == 0)
                {
                    res.Add(initial);
                    return res;
                }
                bool found = false;
                foreach(var range in ranges)
                {
                    if(initial.Item1 > range.Item1 && initial.Item1 < range.Item2 && initial.Item2 > range.Item2)
                    {
                        res.Add((range.Item1, initial.Item2));
                        found = true;
                        break;
                    }
                    else if(initial.Item2 < range.Item2 && initial.Item2 > range.Item1 && initial.Item1 < range.Item1)
                    {
                        res.Add((initial.Item1, range.Item2));
                        found = true;
                        break;
                    }
                    else
                    {
                        res.Add(range);
                    }
                }
                if(!found)
                {
                    res.Add(initial);
                }
                return res;
            }

            while (x.MoveNext())
            {
                long seed = long.Parse(x.Current);
                if(start == -1)
                    start = seed;
                else
                {
                    ranges = splitt((start, seed+start)).ToList();
                    start = -1;
                }
            }

            parseMaps(spanInput);
            return Solve(ranges);
        }

        struct Map
        {
            public long from;
            public long to;
            public long adjust;
            
            internal static Map Parse(ReadOnlySpan<char> line)
            {
                Span<Range> range = stackalloc Range[3];
                var t = line.Split(range,' ',StringSplitOptions.RemoveEmptyEntries);
                return new Map()
                {
                    from = long.Parse(line[range[1]]),
                    to = long.Parse(line[range[1]])+long.Parse(line[range[2]])-1,
                    adjust = long.Parse(line[range[0]]) - long.Parse(line[range[1]]),
                };
            }
        }
    }
}
