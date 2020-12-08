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
            Console.Clear();
            string[] bootCode = File.ReadAllLines("../../../boot_code.txt");
            AccumulatorValueOnInfiniteLoop(bootCode);
            FixCorruptedInstruction(bootCode);

        }

        // Part 1
        static void AccumulatorValueOnInfiniteLoop(string[] bootCode)
        {
            int acc = 0;
            List<int> executedInstructions = new List<int>();

            for(int i = 0; i < bootCode.Length; i++)
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

            Console.WriteLine($"Accumulator Value: {acc}");

        }

        // Part 2
        static void FixCorruptedInstruction(string[] bootCode)
        {

            int lastAlteredInstruction = 0;

            while(lastAlteredInstruction < bootCode.Length)
            {
                string[] bootCodeCopy = new string[bootCode.Length];
                bootCode.CopyTo(bootCodeCopy, 0);

                string[] data = bootCodeCopy[lastAlteredInstruction].Split(' ');
                string instruction = data[0];
                int value = int.Parse(data[1]);
            
                if (instruction == "jmp")
                    bootCodeCopy[lastAlteredInstruction] = "nop " + value.ToString();

            
                else if (instruction == "nop")
                    bootCodeCopy[lastAlteredInstruction] = "jmp " + value.ToString();
                

                lastAlteredInstruction++;


                int[] executionData = ExecuteBootCode(bootCodeCopy);
                //Console.WriteLine(lastExecutedInstruction);
                if (executionData[0] == bootCode.Length - 1)
                {
                    Console.WriteLine($"Accumulator value after fix: {executionData[1]}");
                    break;
                }

            }

        }

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

    }
}
