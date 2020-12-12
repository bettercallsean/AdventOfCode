using System;
using System.IO;
using System.Linq;
using Shared;

namespace Day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] chairsString = File.ReadAllLines("../../../chairs.txt");
            string[,] chairs = Tools.CreateTwoDimensionalArray(chairsString);

            FindNumberOfEmptyChairs(chairs);
        }

        static void FindNumberOfEmptyChairs(string[,] chairs)
        {
            int occupiedSeats = 0;
            bool stateChanged = true;

            int row = chairs.GetLength(0);
            int col = chairs.GetLength(1);

            while (stateChanged)
            {
                string[,] copy = Tools.Create2DArrayCopy(chairs);
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (chairs[i, j] != ".")
                        {
                            // Part 1
                            //copy[i, j] = ChangeCellState(chairs, i, j);

                            // Part 2
                            copy[i, j] = ChangeCellState(chairs, i, j);

                            // Seat has become occupied
                            if (copy[i, j] == "#" && chairs[i, j] == "L")
                                occupiedSeats++;

                            // Seat has become unoccupied
                            else if (copy[i, j] == "L" && chairs[i, j] == "#")
                                occupiedSeats--;
                        }

                    }
                }

                if (Tools.Compare2DArrays(chairs, copy))
                    stateChanged = false;

                chairs = copy;

            }
            Console.WriteLine(occupiedSeats);
        }


        static string ChangeCellState(string[,] array, int i, int j)
        {
            string layout = array[i, j];
            string adjacents = GetAdjacents(array, i, j);
            switch (layout)
            {
                case "L":
                    if (!adjacents.Contains('#'))
                        return "#";

                    break;
                case "#":
                    if (adjacents.Count(x => x == '#') >= 4)
                        return "L";
                    break;
            }

            return array[i, j];
        }

        static string ChangeCellStatePart2(string[,] array, int i, int j)
        {
            string layout = array[i, j];
            string adjacents = GetAdjacentsPart2(array, i, j);
            switch (layout)
            {
                case "L":
                    if (!adjacents.Contains('#'))
                        return "#";

                    break;
                case "#":
                    if (adjacents.Count(x => x == '#') >= 5)
                        return "L";
                    break;
            }

            return array[i, j];
        }

        // Part 1
        static string GetAdjacents(string[,] array, int i, int j)
        {
            string adjacents = "";

            if (CheckNeighbour(i, j, array))
            {
                // Checks row above
                if (CheckNeighbour(i - 1, j - 1, array))
                    adjacents += array[i - 1, j - 1];
                if (CheckNeighbour(i - 1, j, array))
                    adjacents += array[i - 1, j];
                if (CheckNeighbour(i - 1, j + 1, array))
                    adjacents += array[i - 1, j + 1];

                // Check current row
                if (CheckNeighbour(i, j - 1, array))
                    adjacents += array[i, j - 1];
                if (CheckNeighbour(i, j + 1, array))
                    adjacents += array[i, j + 1];

                // Check row below
                if (CheckNeighbour(i + 1, j - 1, array))
                    adjacents += array[i + 1, j - 1];
                if (CheckNeighbour(i + 1, j, array))
                    adjacents += array[i + 1, j];
                if (CheckNeighbour(i + 1, j + 1, array))
                    adjacents += array[i + 1, j + 1];
            }

            return adjacents;
        }

        // Part 2
        static string GetAdjacentsPart2(string[,] array, int i, int j)
        {
            // This bit is incredibly slow but for this one I really couldn't be bothered.
            // Not sure if the challenge is supposed to be slow, but it only takes a few seconds

            string adjacents = "";
            bool[,] adjacentsSet = { { false, false, false},
                                    { false, true, false},
                                    { false, false, false} };

            int count = 1;

            while(count < array.GetLength(0))
            {
                if (CheckNeighbour(i, j, array))
                {
                    // Checks row above
                    if (CheckNeighbour(i - count, j - count, array) && array[i - count, j - count] != "." && !adjacentsSet[0, 0])
                    {
                        adjacents += array[i - count, j - count];
                        adjacentsSet[0, 0] = true;
                    }
                    if (CheckNeighbour(i - count, j, array) && array[i - count, j] != "." && !adjacentsSet[0, 1])
                    {
                        adjacents += array[i - count, j];
                        adjacentsSet[0, 1] = true;
                    }
                    if (CheckNeighbour(i - count, j + count, array) && array[i - count, j + count] != "." && !adjacentsSet[0, 2])
                    {
                        adjacents += array[i - count, j + count];
                        adjacentsSet[0, 2] = true;
                    }

                    // Check current row
                    if (CheckNeighbour(i, j - count, array) && array[i, j - count] != "." && !adjacentsSet[1, 0])
                    {
                        adjacents += array[i, j - count];
                        adjacentsSet[1, 0] = true;
                    }
                    if (CheckNeighbour(i, j + count, array) && array[i, j + count] != "." && !adjacentsSet[1, 2])
                    {
                        adjacents += array[i, j + count];
                        adjacentsSet[1, 2] = true;
                    }

                    // Check row below
                    if (CheckNeighbour(i + count, j - count, array) && array[i + count, j - count] != "." && !adjacentsSet[2, 0])
                    {
                        adjacents += array[i + count, j - count];
                        adjacentsSet[2, 0] = true;
                    }
                    if (CheckNeighbour(i + count, j, array) && array[i + count, j] != "." && !adjacentsSet[2, 1])
                    {
                        adjacents += array[i + count, j];
                        adjacentsSet[2, 1] = true;
                    }
                    if (CheckNeighbour(i + count, j + count, array) && array[i + count, j + count] != "." && !adjacentsSet[2, 2])
                    {
                        adjacents += array[i + count, j + count];
                        adjacentsSet[2, 2] = true;
                    }
                }

                count++;
            }

            return adjacents;
        }

        static bool CheckNeighbour(int i, int j, string[,] array)
        {
            int matrixHeight = array.GetLength(0);
            int matrixWidth = array.GetLength(1);

            if (i >= 0 && i < matrixHeight && j >= 0 && j < matrixWidth)
            {
                return true;
            }
            return false;
        }
    }
}
