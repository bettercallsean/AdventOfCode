using System;
using System.IO;

namespace Day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] map = File.ReadAllLines("../../../map.txt");
            int[][] slopes = new int[][] { 
                new int[] { 1, 1 }, 
                new int[] { 3, 1 },
                new int[] { 5, 1 },
                new int[] { 7, 1 },
                new int[] { 1, 2 } 
            };

            CountNumberOfTrees(map);
            ProductOfMultipleSlopeTreeCounts(map, slopes);
        }

        // Part 1
        static void CountNumberOfTrees(string[] map)
        {
            int treeCount = 0;
            int xCoord = 0;

            for(int i = 0; i < map.Length; i++)
            {
                if (map[i][xCoord] == '#')
                    treeCount++;

                xCoord += 3;
                // There's 31 characters per string, but arrays are zero-based, so if xCoord becomes greater than the max, zero-based index,
                // the string length (31) is subtracted to put the index back at with the correct zero-based value
                // e.g Index 33 is the same as accessing index 2 from the string, so subtracting 31 gives us 2, putting the index back
                // at the start of the string
                if (xCoord > map[i].Length - 1)
                    xCoord -= map[i].Length;

            }

            Console.WriteLine("Part 1: {0}", treeCount);
        }

        // Part 2
        static void ProductOfMultipleSlopeTreeCounts(string[] map, int[][] slopes)
        {
            

            long product = 1;
            foreach (int[] slope in slopes)
            {
                int treeCount = 0;
                int xCoord = 0;
                for (int i = 0; i < map.Length; i += slope[1])
                {
                    if (map[i][xCoord] == '#')
                        treeCount++;

                    xCoord += slope[0];
                    // There's 31 characters per string, but arrays are zero-based, so if xCoord becomes greater than the max, zero-based index,
                    // the string length (31) is subtracted to put the index back at with the correct zero-based value
                    // e.g Index 33 is the same as accessing index 2 from the string, so subtracting 31 gives us 2, putting the index back
                    // at the start of the string
                    if (xCoord > map[i].Length - 1)
                        xCoord -= map[i].Length;

                }

                product *= treeCount;

            }

            Console.WriteLine("Part 2: {0}", product);


            
        }
    }
}
