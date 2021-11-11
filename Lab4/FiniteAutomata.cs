using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab4
{
    public class FiniteAutomata
    {
        public List<string> Q { get; }
        public List<string> E { get; }
        public List<string> F { get; }

        public List<Tuple<Tuple<string,string>, string>> S { get; } = new();

        private string q0;

        public FiniteAutomata(string faFilePath)
        {
            using var file = new StreamReader(faFilePath);

            var lines = File.ReadAllLines(faFilePath);
            
            Q = new List<string>(lines[0].Split(" ") ?? Array.Empty<string>());
            Q.Remove("Q");
            Q.Remove("=");

            E = new List<string>(lines[1].Split(" ") ?? Array.Empty<string>());
            E.Remove("E");
            E.Remove("=");

            q0 = lines[2].Split(" ")[^1];

            F = new List<string>(lines[3].Split(" ") ?? Array.Empty<string>());
            F.Remove("F");
            F.Remove("=");

            for (var i = 5; i < lines.Length; i++)
            {
                var s = lines[i].Split(',', '(', ')', ' ', '-', '>', '\t');
                
                var elements = s.Where(item => item != "").ToList();

                var key = new Tuple<string, string>(elements[0], elements[1]);
                var value = elements[2];

                S.Add(new Tuple<Tuple<string, string>, string>(key, value));
            }
        }

        public bool CheckIfDFA()
        {
            foreach (var element in S)
            {
                var key = element.Item1;
                
                var found = S.Count(toCheck => Equals(toCheck.Item1, key));

                if (found != 1)
                    return false;
            }

            return true;
        }
    }
}