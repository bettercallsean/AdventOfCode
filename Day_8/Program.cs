using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] bootCode = File.ReadAllLines("../../../boot_code.txt");

            int[] bootData = ExecuteBootCode(bootCode);
            Console.WriteLine($"Accumulator Value: {bootData[1]}");
            
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            FixCorruptedInstructionsImproved(bootCode);

            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        // Part 1
        static int[] ExecuteBootCode(string[] bootCode)
        {
            int acc = 0;
            List<int> executedInstructions = new List<int>();

            for (int i = 0; i < bootCode.Length; i++)
            {
                if (executedInstructions.Contains(i))
                    break;

                string[] data = bootCode[i].Split(' ');
                string instruction = data[0];
                int value = int.Parse(data[1]);

                executedInstructions.Add(i);
                switch (instruction)
                {
                    case ("acc"):
                        acc += value;
                        break;
                    case ("jmp"):
                        i += value - 1;
                        break;
                    case ("nop"):
                        break;
                }
            }

            int[] executionData = { executedInstructions.Last(), acc };
            return executionData;

        }

        // Part 2 - Improved
        static void FixCorruptedInstructionsImproved(string[] bootCode)
        {
            List<int> executedInstructions = GetExecutedInstructions(bootCode);

            foreach(int instructionIndex in executedInstructions)
            {
                string[] bootCodeCopy = new string[bootCode.Length];
                bootCode.CopyTo(bootCodeCopy, 0);

                string[] data = bootCodeCopy[instructionIndex].Split(' ');
                string instruction = data[0];
                int value = int.Parse(data[1]);

                if (instruction == "jmp")
                    bootCodeCopy[instructionIndex] = "nop " + value.ToString();

                else if (instruction == "nop")
                    bootCodeCopy[instructionIndex] = "jmp " + value.ToString();

                int[] executionData = ExecuteBootCode(bootCodeCopy);
                if (executionData[0] == bootCode.Length - 1)
                {
                    Console.WriteLine($"Accumulator value after fix: {executionData[1]}");
                    break;
                }
            }

        }

        static List<int> GetExecutedInstructions(string[] bootCode)
        {
            int acc = 0;
            List<int> executedInstructions = new List<int>();

            for (int i = 0; i < bootCode.Length; i++)
            {
                if (executedInstructions.Contains(i))
                    break;

                string[] data = bootCode[i].Split(' ');
                string instruction = data[0];
                int value = int.Parse(data[1]);

                executedInstructions.Add(i);
                switch (instruction)
                {
                    case ("acc"):
                        acc += value;
                        break;
                    case ("jmp"):
                        i += value - 1;
                        break;
                    case ("nop"):
                        break;
                }
            }

            return executedInstructions;
        }

    }
}
