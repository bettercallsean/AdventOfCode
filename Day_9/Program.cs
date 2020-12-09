using System;
using System.Collections.Generic;
using System.IO;

namespace Day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] stringData = File.ReadAllLines("../../../xmas_data.txt");
            long[] data = StringToIntArray(stringData);

            long invalidNumber = FindInvalidNumber(data);
            ContingousSetOfNumbers(invalidNumber, data);
        }
        
        //Part 1 
        static long[] StringToIntArray(string[] stringData)
        {
            long[] data = new long[stringData.Length];

            for (int i = 0; i < stringData.Length; i++)
            {
                data[i] = Convert.ToInt64(stringData[i]);
            }

            return data;
        }

        static long FindInvalidNumber(long[] data)
        {
            int startIndex = 0;
            while(startIndex < data.Length)
            {
                long[] range = CreateRangeOfData(data, startIndex, 25);
                List<long> validNumbers = ValidNumbers(range);
                
                if(!validNumbers.Contains(data[startIndex + 25]))
                {
                    Console.WriteLine($"Invalid Number: {data[startIndex + 25]}");
                    break;
                }

                startIndex++;
            }

            return data[startIndex + 25];
        }
        
        static List<long> ValidNumbers(long[] dataRange)
        {
            List<long> validNumbers = new List<long>();

            for(int i = 0; i < dataRange.Length; i++)
            {
                for(int j = 0; j < dataRange.Length; j++)
                {
                    if (i == j)
                        continue;

                    long number = dataRange[i] + dataRange[j];
                    if (!validNumbers.Contains(number))
                        validNumbers.Add(number);
                }
            }

            return validNumbers;
        }

        static long[] CreateRangeOfData(long[] data, int startingIndex, int dataLength)
        {
            long[] range = new long[dataLength];

            for(int i = 0; i < dataLength; i++)
            {
                range[i] = data[startingIndex++];
            }

            return range; 
        }

        // Part 2
        static void ContingousSetOfNumbers(long number, long[] data)
        {
            // Used to set the startingIndex
            for(int i = 0; i < data.Length; i++)
            {
                // Used to set dataLength
                for(int j = 2; j < data.Length - i; j ++)
                {
                    long sum = 0;
                    long[] setOfNumbers = CreateRangeOfData(data, i, j);
                    bool valuesTooBig = false;

                    for (int k = 0; k < setOfNumbers.Length; k++)
                    {
                        if(setOfNumbers[k] >= number)
                        {
                            valuesTooBig = true;
                            break;
                        }

                        sum += setOfNumbers[k];
                    }

                    if (valuesTooBig)
                        break;

                    Array.Sort(setOfNumbers);

                    if (sum == number)
                    {
                        Console.WriteLine($"The encryption weakness is: {setOfNumbers[0] + setOfNumbers[^1]}");
                        return;
                    }

                }

            }
        }
    }
}
