using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_6
{
    class Program
    { 

        static void Main(string[] args)
        {
            SumOfUniqueGroupAnswers("../../../customs_answers.txt");
            SumOfAllGroupAnswers("../../../customs_answers.txt");
        }

        // Part 1
        static void SumOfUniqueGroupAnswers(string filename)
        {
            int sum = 0;
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                List<char> answers = new List<char>();

                while((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        sum += answers.Count;
                        answers.Clear();
                    }
                    else
                    {
                        char[] individualAnswers = line.ToCharArray();
                        foreach (char answer in individualAnswers)
                        {
                            if (!answers.Contains(answer))
                                answers.Add(answer);
                        }
                    }
                }
            }

            Console.WriteLine($"Part 1: {sum}");
        }

        // Part 2
        static void SumOfAllGroupAnswers(string filename)
        {
            int sum = 0;
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                string groupAnswers = "";
                int groupCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

                        foreach (char letter in alpha)
                            sum += groupAnswers.Count(c => c == letter) == groupCount ? 1 : 0;
                        

                        groupAnswers = "";
                        groupCount = 0;
                    }
                    else
                    {
                        groupCount++;
                        groupAnswers += line;
                    }
                }
            }

            Console.WriteLine($"Part 2: {sum}");
        }
    }
}
