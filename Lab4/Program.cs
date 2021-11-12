using System;
using System.Linq;

namespace Lab4
{
    class Program
    {
        /// <summary>
        /// Runs the application by printing the list of available commands while waiting for input only if the
        /// initialized automaton is valid
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var fa = new FiniteAutomaton("FA.in");

            if (!fa.CheckIfValid())
            {
                Console.WriteLine("Not a valid FA");
                goto Exit;
            }
            
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Display states");
                Console.WriteLine("2. Display alphabet");
                Console.WriteLine("3. Display transitions");
                Console.WriteLine("4. Display final states");
                Console.WriteLine("5. Check if the sequence is accepted");

                var command = Convert.ToInt32(Console.ReadLine());

                switch (command)
                {
                    case 0:
                        goto Exit;
                    case 1:
                        PrintStates(fa);
                        break;
                    case 2:
                        PrintAlphabet(fa);
                        break;
                    case 3:
                        PrintTransition(fa);
                        break;
                    case 4:
                        PrintFinalStates(fa);
                        break;
                    case 5:
                        CheckSequence(fa);
                        break;
                }
            }

            Exit:
            Console.WriteLine("Exiting program");
        }

        /// <summary>
        /// Gets the list of states from a given FA and prints it
        /// </summary>
        /// <param name="fa">A given FA</param>
        private static void PrintStates(FiniteAutomaton fa)
        {
            Console.WriteLine();
            var result = fa.Q.Aggregate("States: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        /// <summary>
        /// Gets the alphabet from a given FA and prints it
        /// </summary>
        /// <param name="fa">A given FA</param>
        private static void PrintAlphabet(FiniteAutomaton fa)
        {
            Console.WriteLine();
            var result = fa.E.Aggregate("Alphabet: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        /// <summary>
        /// Gets the list of transitions from a given FA and prints it
        /// </summary>
        /// <param name="fa">A given FA</param>
        private static void PrintTransition(FiniteAutomaton fa)
        {
            Console.WriteLine();
            var result = fa.S.Aggregate("Transitions: \n", (current, item) => current + item + " \n");

            Console.WriteLine(result);
        }

        /// <summary>
        /// Gets the list of final states from a given FA and prints it
        /// </summary>
        /// <param name="fa">A given FA</param>
        private static void PrintFinalStates(FiniteAutomaton fa)
        {
            Console.WriteLine();
            var result = fa.F.Aggregate("Final states: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        /// <summary>
        /// Checks if a sequence if valid. First we need to determine if the FA si a DFA. If it's not we print a message
        /// otherwise we start checking the sequence.
        /// </summary>
        /// <param name="fa"></param>
        private static void CheckSequence(FiniteAutomaton fa)
        {
            if (!fa.CheckIfDFA())
            {
                Console.WriteLine("Not DFA");
                return;
            }
            
            Console.WriteLine("DFA");
            
        }
    }
}