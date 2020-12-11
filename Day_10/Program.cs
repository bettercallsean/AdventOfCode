using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] adapterJoltsString = File.ReadAllLines("../../../adapter_jolts.txt");
            int[] adapterJolts = StringToIntArray(adapterJoltsString);
            Array.Sort(adapterJolts);

            JoltDifferencesMultiplied(adapterJolts);
            AdapterArrangments(adapterJolts);

        }

        static int[] StringToIntArray(string[] stringData)
        {
            int[] data = new int[stringData.Length + 1];
            data[0] = 0;

            for (int i = 1; i < stringData.Length + 1; i++)
            {
                data[i] = int.Parse(stringData[i - 1]);
            }

            return data;
        }

        // Part 1
        static void JoltDifferencesMultiplied(int[] adapters)
        {
            int oneJoltDifference = 0;
            int threeJoltDifference = 0;
            int joltage = 0;

            for(int i = 0; i < adapters.Length; i++)
            {
                if (adapters[i] - joltage == 1)
                    oneJoltDifference++;

                else if (adapters[i] - joltage == 3)
                    threeJoltDifference++;

                joltage = adapters[i];
            }

            // Takes into account the device adapter's 3-higher jolt rating
            threeJoltDifference++;

            Console.WriteLine($"Part 1: {oneJoltDifference * threeJoltDifference}");
        }

        // Part 2
        static void AdapterArrangments(int[] adapters)
        {
            int sevenPaths = 0;
            int twoPaths = 0;

            // This one required some thinking, yikes. You need to think of this as a tree/graph
            for (int i = 0; i < adapters.Length - 3; i++)
            {
                // If the node +3 ahead has a difference of 3, that means there's two
                // nodes before them
                //
                //       0
                //     / | \
                //    1--2--3
                //    \-----/ (node 1 can connect to node 3 because there is a difference of <3)
                //  
                if (adapters[i + 3] - adapters[i] == 3)
                {
                    i += 3;
                    // The next node after that (4th node along) could connect to the 3rd
                    // to create 7 possible paths to go down if it has a difference of only 1
                    //
                    //          0
                    //        / | \
                    //       1--2--3
                    //       \-----/
                    //        \-4-/
                    if (adapters[i + 1] - adapters[i] == 1)
                        sevenPaths++;
                    // If not, then the 3rd node has 4 possible paths to be reached from
                    else
                        twoPaths += 2;

                }
                // If the above conditions aren't met, there could still be multiple paths.
                // If the node 2 ahead has a difference of two from the current one, then there is
                // 1 node inbetween, meaning there are two paths that could be used to reach the second node
                //
                //      9
                //      |
                //      10
                //     /  \
                //    11--12
                //         |
                //        15

                else if(adapters[i + 2] - adapters[i] == 2)
                {
                    twoPaths++;
                    i += 2;
                }

            }

            Console.WriteLine($"Part 2: {Math.Pow(2, twoPaths) * Math.Pow(7, sevenPaths)}");
        }



    }
}
