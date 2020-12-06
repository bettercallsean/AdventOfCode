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
            string[] groupAnswers = File.ReadAllText(filename).Split("\n\n");
            foreach(string answer in groupAnswers)
            {
                sum += answer.Replace("\n", "").Distinct().Count();
            }    

            Console.WriteLine($"Part 1: {sum}");
        }

        // Part 2
        static void SumOfAllGroupAnswers(string filename)
        {
            int sum = 0;
            string[] groupAnswers = File.ReadAllText(filename).Split("\n\n");
            foreach(string answer in groupAnswers)
            {
                // Each new peson is on a new line, so counting all of the newlines + 1 for the person at the top
                // will give us a value that can be used to find which characters are available in all of the strings.
                int groupCount = answer.Count(c => c == '\n') + 1;
                
                // Groups by characters that are present in all of the groups answers
                sum += answer.GroupBy(x => x).Where(x => x.Count() == groupCount).Count();
            }

            Console.WriteLine($"Part 2: {sum}");
        }
    }
}
