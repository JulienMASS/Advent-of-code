// See https://aka.ms/new-console-template for more information
using Advent_of_code_2023;
using System.Diagnostics;
using System.Reflection;
using System.Resources;


Console.WriteLine("Bienvenue dans cette nouvelle édition de Advent of code 2023");
do
{
    Console.WriteLine("Quel jour voulez-vous lancer ?");
    int day = int.Parse(Console.ReadLine()!);
    string className = $"Day{day}";
    string input = Advent_of_code_2023.Properties.Resources.ResourceManager.GetString($"Day{day}")!;
    Type? type = Assembly.GetExecutingAssembly().GetType($"Advent_of_code_2023.{className}");
    if (type == null || string.IsNullOrEmpty(input))
    {
        Console.WriteLine($"Le jour {day} n'existe pas, ou n'a pas d'input");
    }
    else
    {
        string[] lines = input.Split("\r\n");
        object? instance = Activator.CreateInstance(type);
        MethodInfo? method = type.GetMethod("SolvePart1");
        if (method == null)
        {
            Console.WriteLine($"Le jour {day} n'a pas d'implémentation de la partie 1");
        }

        Stopwatch sw = Stopwatch.StartNew();
        object? res1 = null;
            try { res1 = method?.Invoke(instance, new object[] { lines }) ?? "?"; }catch(Exception e) { res1 = e.Message; }
            
        string elapsed1 = GetStopWatchString(sw);
        MethodInfo? method2 = type.GetMethod("SolvePart2");
        if (method == null)
        {
            Console.WriteLine($"Le jour {day} n'a pas d'implémentation de la 2ème solution");
        }
        sw.Restart();
        var res2 = method2?.Invoke(instance, new object[] { lines }) ?? "?";
        string elapsed2 = GetStopWatchString(sw);
        sw.Stop();
        DrawResult($"Résultat de la partie 1 : {res1}, temps : {elapsed1}", $"Résultat de la partie 2 : {res2}, temps : {elapsed2}");
    }
}while (true);

//Return millisecond string 
string GetStopWatchString(Stopwatch sw)
{
    var miliseconds = (sw.Elapsed.TotalNanoseconds > 1000000) ? sw.Elapsed.TotalMilliseconds : sw.Elapsed.TotalNanoseconds / 1000000;
    return  $"{miliseconds:N4} ms";
}

void DrawResult(string part1,string part2)
{
    Console.WriteLine("╔" + new string('═', Math.Max(part1.Length, part2.Length)) + "╗");
    Console.WriteLine($"║{part1+new string(' ',Math.Max(0,part2.Length-part1.Length))}║");
    Console.WriteLine("╠" + new string('═', Math.Max(part1.Length, part2.Length)) + "╣");
    Console.WriteLine($"║{part2 + new string(' ', Math.Max(0,part1.Length - part2.Length))}║");
    Console.WriteLine("╚" + new string('═', Math.Max(part1.Length, part2.Length)) + "╝");
}