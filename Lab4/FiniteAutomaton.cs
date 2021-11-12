using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab4
{
    /// <summary>
    /// Representation of a finite automaton. Q: list of states, E: list of input symbols, q0: string of initial state
    /// F: list of final states, S: list of transitions
    /// </summary>
    public class FiniteAutomaton
    {
        public List<string> Q { get; }
        public List<string> E { get; }
        public List<string> F { get; }

        public List<Tuple<Tuple<string, string>, string>> S { get; } = new();

        private string q0;

        /// <summary>
        /// Constructor gets a filePath, reads from it line by line and initializes Q,E,F,q0 and S
        /// </summary>
        /// <param name="faFilePath"></param>
        public FiniteAutomaton(string faFilePath)
        {
            using var file = new StreamReader(faFilePath);

            var lines = File.ReadAllLines(faFilePath);

            Q = new List<string>(lines[0].Split(" "));
            Q.Remove("Q");
            Q.Remove("=");

            E = new List<string>(lines[1].Split(" "));
            E.Remove("E");
            E.Remove("=");

            q0 = lines[2].Split(" ")[^1];

            F = new List<string>(lines[3].Split(" "));
            F.Remove("F");
            F.Remove("=");

            for (var i = 5; i < lines.Length; i++)
            {
                var s = lines[i].Split(',', '(', ')', ' ', '-', '>', '\t');

                var elements = s.Where(item => item != "").ToList();

                var key = new Tuple<string, string>(elements[0], elements[1]);
                var value = elements[2];

                if (!S.Contains(new Tuple<Tuple<string, string>, string>(key, value))) 
                    S.Add(new Tuple<Tuple<string, string>, string>(key, value));
            }
            
        }

        /// <summary>
        /// Checks if the read FA is a valid one
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool CheckIfValid()
        {
            if (!Q.Contains(q0))
                return false;

            if (F.Any(state => !Q.Contains(state)))
            {
                return false;
            }

            foreach (var (key, value) in S)
            {
                if (!Q.Contains(key.Item1) || !E.Contains(key.Item2) || !Q.Contains(value))
                    return false;
            }
            
            return true;
        }

        /// <summary>
        /// Checks if a FA is DFA by making sure that there no more than one transitions with a given input from a state
        /// to another
        /// </summary>
        /// <returns>True if DFA, false otherwise</returns>
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