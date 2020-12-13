using System;
using System.Collections.Generic;
using System.IO;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> day1Input = InputToList("../../../day_1_input.txt");
            FindTwoValuesToMake2020(day1Input);
            FindThreeValuesToMake2020(day1Input);
        }

        private static List<int> InputToList(string filename)
        {
            List<int> day1Input = new List<int>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int.TryParse(line, out int number);
                    day1Input.Add(number);
                }
            }

            // Sorts to make searching for values quicker with binary search
            day1Input.Sort();

            return day1Input;
        }

        // Part 1 Solution
        private static void FindTwoValuesToMake2020(List<int> day1Input)
        {
            foreach (int number in day1Input)
            {
                // We can use binary search to quickly see if secondNumberToFind is present in the list,
                // we don't need the index because we just need to know if the value is present
                int secondNumberToFind = 2020 - number;
                if (day1Input.BinarySearch(secondNumberToFind) >= 0)
                {
                    int answer = number * secondNumberToFind;
                    Console.WriteLine("Part 1: {0}", answer);
                    break;
                }

            }

        }

        // Part 2 Solution
        private static void FindThreeValuesToMake2020(List<int> day1Input)
        {

            foreach (int number in day1Input)
            {
                // We'll try to find two numbers that add up to valueToFind
                int valueToFind = 2020 - number;

                foreach (int secondNumber in day1Input)
                {
                    // We can't use the same number twice, so we'll skip it
                    if (secondNumber == number)
                        continue;

                    // Search the list to see if thirdValueToFind is in the list,
                    // the actual index isn't needed because we just need to know if the value is present
                    int thirdValueToFind = valueToFind - secondNumber;
                    if (day1Input.BinarySearch(thirdValueToFind) >= 0)
                    {
                        int answer = number * secondNumber * thirdValueToFind;
                        Console.WriteLine("Part 2: {0}", answer);

                        return;
                    }

                }
            }
        }
    }
}
