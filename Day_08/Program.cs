using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
    class ExecutionData
    {
        public ExecutionData()
        {
            ExecutedInstructions = new List<int>();
        }

        public int LastExecutedInstruction { get; set; }
        public List<int> ExecutedInstructions { get; set; }
        public int AccumulatorValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] bootCode = File.ReadAllLines("../../../boot_code.txt");

            ExecutionData bootData = ExecuteBootCode(bootCode);
            Console.WriteLine($"Accumulator Value: {bootData.AccumulatorValue}");
            
            FixCorruptedInstructions(bootCode, bootData);

        }

        // Part 1
        static ExecutionData ExecuteBootCode(string[] bootCode)
        {
            ExecutionData executionData = new ExecutionData();

            for (int i = 0; i < bootCode.Length; i++)
            {
                if (executionData.ExecutedInstructions.Contains(i))
                    break;

                string[] data = bootCode[i].Split(' ');
                string instruction = data[0];
                int value = int.Parse(data[1]);

                executionData.ExecutedInstructions.Add(i);
                executionData.LastExecutedInstruction = i;
                switch (instruction)
                {
                    case ("acc"):
                        executionData.AccumulatorValue += value;
                        break;
                    case ("jmp"):
                        i += value - 1;
                        break;
                    case ("nop"):
                        break;
                }
            }

            return executionData;

        }

        // Part 2
        static void FixCorruptedInstructions(string[] bootCode, ExecutionData bootData)
        {
            foreach(int instructionIndex in bootData.ExecutedInstructions)
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

                ExecutionData executionData = ExecuteBootCode(bootCodeCopy);
                if (executionData.LastExecutedInstruction == bootCode.Length - 1)
                {
                    Console.WriteLine($"Accumulator value after fix: {executionData.AccumulatorValue}");
                    break;
                }
            }

        }

    }
}
