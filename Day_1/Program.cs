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
            int answer = FindValueToMake2020(day1Input);
            int answer2 = FindThreeValuesToMake2020(day1Input);

            Console.WriteLine("Part 1: {0}", answer);
            Console.WriteLine("Part 2: {0}", answer2);
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
        private static int FindValueToMake2020(List<int> day1Input)
        {
            int answer = 0;
            foreach (int number in day1Input)
            {
                // We can use binary search to quickly see if secondNumberToFind is present in the list
                int secondNumberToFind = 2020 - number;
                int secondValueIndex;
                if ((secondValueIndex = day1Input.BinarySearch(secondNumberToFind)) >= 0)
                {
                    answer = number * secondNumberToFind;
                    break;
                }

            }

            return answer;
        }

        // Part 2 Solution
        private static int FindThreeValuesToMake2020(List<int> day1Input)
        {
            int answer = 0;
            foreach (int number in day1Input)
            {
                // We'll try to find two numbers that add up to valueToFind
                int valueToFind = 2020 - number;

                foreach (int secondNumber in day1Input)
                {
                    // We can't use the same number twice, so we'll skip it
                    if (secondNumber == number)
                        continue;

                    // Search the list to see if thirdValueToFind is in the list
                    int thirdValueToFind = valueToFind - secondNumber;
                    int thirdValueIndex;
                    if ((thirdValueIndex = day1Input.BinarySearch(thirdValueToFind)) >= 0)
                    {
                        answer = number * secondNumber * thirdValueToFind;
                        break;
                    }

                }
            }

            return answer;
        }
    }
}
