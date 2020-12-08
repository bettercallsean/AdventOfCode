using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_7
{

    class Program
    {
        public static List<string> luggageRules = File.ReadAllLines("../../../luggage_rules.txt").ToList();
        
        static void Main(string[] args)
        {
            List<string> bagsToSearchFor = new List<string> { "shiny gold" };
            List<string> searched = new List<string>();
            int bagsContainingGold = ParseLuggageRules(0, bagsToSearchFor, searched);
            int bagCount = BagsWithinBags("shiny gold", searched);

            Console.WriteLine($"Part 1: {bagsContainingGold}");
            Console.WriteLine($"Part 2: {bagCount}");
        }

        // Part 1
        static int ParseLuggageRules(int bagsContainingShinyGold, List<string> bagsToSearchFor, List<string> searched)
        {
            List<string> bagList = new List<string>();


            foreach (string bagColour in bagsToSearchFor)
            {
                //Console.Write(bagColour + " - ");
                foreach (string rule in luggageRules)
                {
                    string[] bags = rule.Split("contain");

                    string bag = bags[0][0..^6];
                    string bagContents = bags[1];

                    if (bagContents.Contains(bagColour) && !searched.Contains(bag))
                    {
                        bagList.Add(bag);
                        searched.Add(bag);
                    }

                }
            }

            return bagsContainingShinyGold += bagList.Count == 0 ? 0 : ParseLuggageRules(bagList.Count, bagList, searched);
            

        }

        // Part 2
        static int BagsWithinBags(string bagToSearchFor, List<string>searched)
        {
            List<string> bagList = new List<string>();
            int count = 0;

            foreach (string rule in luggageRules)
            {
                string[] bags = rule.Split(" contain ");

                string bag = bags[0][0..^5];
                string bagContents = Regex.Replace(bags[1]," bag[s]?[.]?", "");

                if(bag.Contains(bagToSearchFor) && bagContents != "no other")
                    bagList = bagContents.Split(", ").ToList();
                

            }

            foreach(string contents in bagList)
            {
                int quantity = (int)char.GetNumericValue(contents[0]);

                string bag = contents[2..].Replace(".", "");
                count += quantity + (quantity * BagsWithinBags(bag, searched));
            }

            return count;

        }
    }
}
