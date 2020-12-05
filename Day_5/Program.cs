using System;
using System.Collections.Generic;
using System.IO;

namespace Day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] boardPasses = File.ReadAllLines("../../../boarding_passes.txt");
            SeatFinder(boardPasses);
        }

        static void SeatFinder(string[] boardingPasses)
        {
            int highestSeatId = 0;
            List<int> seatIds = new List<int>();

            foreach (string pass in boardingPasses)
            {
                int row;
                int high = 127;
                int low = 0;

                for(int i = 0; i < 7; i++)
                {
                    if (pass[i] == 'F')
                        high = (low + high) / 2;
                    else
                        low = (high + 1 + low) / 2;
                }

                row = low;
                high = 7;
                low = 0;

                for(int i = 7; i < 10; i++)
                {
                    if (pass[i] == 'L')
                        high = (low + high) / 2;
                    else
                        low = (high + 1 + low) / 2;
                }

                int seatId = row * 8 + low;
                seatIds.Add(seatId);

                if (seatId > highestSeatId)
                    highestSeatId = seatId;

            }


            Console.WriteLine("Highest Seat Id: {0}", highestSeatId);

            seatIds.Sort();
            SeatFinder(seatIds);

        }

        static void SeatFinder(List<int> seatIds)
        {
            for(int i = 1; i < seatIds.Count - 2; i++)
            {
                int seatId = seatIds[i];

                if (seatId + 2 == seatIds[i + 1])
                {
                    Console.WriteLine("Your seat ID: {0}", seatId + 1);
                    break;
                }
            }
        }

    }
}
