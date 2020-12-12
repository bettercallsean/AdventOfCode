using System;

namespace Shared
{
    public class Tools
    {
        public static string[,] CreateTwoDimensionalArray(string[] arr)
        {
            int row = arr.Length;
            int col = arr[0].Length;

            string[,] twoDimensionalArr = new string[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    twoDimensionalArr[i, j] = arr[i][j].ToString();
                }
            }

            return twoDimensionalArr;
        }

        public static string[,] Create2DArrayCopy(string[,] arr)
        {
            int row = arr.GetLength(0);
            int col = arr.GetLength(1);
            string[,] copy = new string[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    copy[i, j] = arr[i, j];
                }
            }

            return copy;
        }

        public static bool Compare2DArrays(string[,] arr1, string[,] arr2)
        {
            int row = arr1.GetLength(0);
            int col = arr1.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (arr1[i, j] != arr2[i, j])
                        return false;
                }
            }

            return true;
        }
    }
    public class Debug
    {
        public static void TwoDimensionArrayPrinter(string[,] arr)
        {
            int row = arr.GetLength(0);
            int col = arr.GetLength(1);

            Console.Write("    ");
            for (int i = 0; i < col; i++)
                Console.Write("{0}  ", i);

            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                Console.Write(i + ": [");
                for (int j = 0; j < col; j++)
                {
                    Console.Write("{0}", arr[i, j]);
                    if (j != col - 1)
                        Console.Write(", ");
                    else
                        Console.Write("]");

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
