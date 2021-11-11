using System;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var fa = new FiniteAutomata("FA.in");

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

        private static void PrintStates(FiniteAutomata fa)
        {
            Console.WriteLine();
            var result = fa.Q.Aggregate("States: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        private static void PrintAlphabet(FiniteAutomata fa)
        {
            Console.WriteLine();
            var result = fa.E.Aggregate("Alphabet: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        private static void PrintTransition(FiniteAutomata fa)
        {
            Console.WriteLine();
            var result = fa.S.Aggregate("Transitions: \n", (current, item) => current + item + " \n");

            Console.WriteLine(result);
        }

        private static void PrintFinalStates(FiniteAutomata fa)
        {
            Console.WriteLine();
            var result = fa.F.Aggregate("Final states: ", (current, item) => current + item + " ");

            Console.WriteLine(result);
        }

        private static void CheckSequence(FiniteAutomata fa)
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