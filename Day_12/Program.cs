using System;
using System.IO;

namespace Day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] directions = File.ReadAllLines("../../../ship_directions.txt");
            ManhattanDistance(directions);
            ManhattanDistancePart2(directions);
        }

        // Part 1
        static void ManhattanDistance(string[] directions)
        {
            int north = 0;
            int east = 0;
            int degrees = 90;

            foreach(string direction in directions)
            {
                char code = direction[0];
                int value = int.Parse(direction[1..]);

                switch(code)
                {
                    case 'N':
                        north += value;
                        break;
                    case 'E':
                        east += value;
                        break;
                    case 'S':
                        north -= value;
                        break;
                    case 'W':
                        east -= value;
                        break;
                    case 'L':
                        if (degrees >= 180)
                            degrees -= value;
                        else
                            degrees += 360 - value;
                        break;
                    case 'R':
                        if (degrees < 180)
                            degrees += value;
                        else
                            degrees -= 360 - value;
                        break;
                    case 'F':
                        if (degrees == 0)
                            north += value;
                        else if (degrees == 180)
                            north -= value;
                        else if (degrees == 90)
                            east += value;
                        else
                            east -= value;
                        break;
                }
            }

            Console.WriteLine($"Part 1: {Math.Abs(north) + Math.Abs(east)}");
        }

        // Part 2
        static void ManhattanDistancePart2(string[] directions)
        {
            int waypointEast = 10;
            int waypointNorth = 1;

            int north = 0;
            int east = 0;

            foreach (string direction in directions)
            {
                char code = direction[0];
                int value = int.Parse(direction[1..]);

                switch (code)
                {
                    case 'N':
                        waypointNorth += value;
                        break;
                    case 'E':
                        waypointEast += value;
                        break;
                    case 'S':
                        waypointNorth -= value;
                        break;
                    case 'W':
                        waypointEast -= value;
                        break;
                    case 'L':
                        int[] coordsLeft = Rotate(waypointEast, waypointNorth, value);
                        waypointEast = coordsLeft[0];
                        waypointNorth = coordsLeft[1];
                        break;
                    case 'R':
                        int[] coordsRight = Rotate(waypointEast, waypointNorth, value * -1);
                        waypointEast = coordsRight[0];
                        waypointNorth = coordsRight[1];
                        break;
                    case 'F':
                        north += waypointNorth * value;
                        east += waypointEast * value;
                        break;
                }
            }

            Console.WriteLine($"Part 2: {Math.Abs(north) + Math.Abs(east)}");
        }

        static int[] Rotate(int east, int north, int degrees)
        {
            double rad = degrees * (Math.PI / 180.0);

            double newEast = Math.Round(east * Math.Cos(rad) - north * Math.Sin(rad));
            double newNorth = Math.Round(north * Math.Cos(rad) + east * Math.Sin(rad));

            int[] newCoords = { (int)newEast, (int)newNorth };
            return newCoords;
        }

    }
}
