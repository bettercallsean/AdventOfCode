using System;
using System.IO;
using System.Linq;

namespace Day_6
{
    class Program
    { 

        static void Main(string[] args)
        {
            string[] groupAnswers = File.ReadAllText("../../../customs_answers.txt").Split("\n\n");
            SumOfUniqueGroupAnswers(groupAnswers);
            SumOfAllGroupAnswers(groupAnswers);
        }

        // Part 1
        static void SumOfUniqueGroupAnswers(string[] groupAnswers)
        {
            int sum = 0;
            foreach(string answer in groupAnswers)
            {
                sum += answer.Replace("\n", "").Distinct().Count();
            }    

            Console.WriteLine($"Part 1: {sum}");
        }

        // Part 2
        static void SumOfAllGroupAnswers(string[] groupAnswers)
        {
            int sum = 0;
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
