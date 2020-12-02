using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> passwords = GetPasswordsFromFile("../../../passwords.txt");
            NumberOfValidPasswords(passwords);
            NumberOfValidPasswordsButNotUsingThePasswordPolicyFromTheSledRentalPlaceDownTheStreet(passwords);
        }

        static List<string> GetPasswordsFromFile(string filename)
        {
            List<string> passwordList = new List<string>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    passwordList.Add(line);
                }
            }

            return passwordList;
        }

        // Part 1
        static void NumberOfValidPasswords(List<string> passwordList)
        {
            int validPasswords = 0;

            foreach(string line in passwordList)
            {
                string[] passwordParameters = line.Split(' ');

                string[] characterRange = passwordParameters[0].Split('-');
                int min = int.Parse(characterRange[0]);
                int max = int.Parse(characterRange[1]);

                char character = passwordParameters[1][0];
                int characterCount = passwordParameters[2].Count(c => c == character);

                if (min <= characterCount && characterCount <= max)
                    validPasswords++;
            }

            Console.WriteLine("Part 1: {0}", validPasswords);
        }

        // Part 2
        static void NumberOfValidPasswordsButNotUsingThePasswordPolicyFromTheSledRentalPlaceDownTheStreet(List<string> passwordList)
        {
            int validPasswords = 0;

            foreach(string line in passwordList)
            {
                string[] passwordParameters = line.Split(' ');

                string[] characterRange = passwordParameters[0].Split('-');
                // The indexes provided aren't zero-based, so we take 1 away to accomodate for this
                int positionOne = int.Parse(characterRange[0]) - 1;
                int positionTwo = int.Parse(characterRange[1]) - 1;

                char character = passwordParameters[1][0];
                string password = passwordParameters[2];

                // Cheeky little XOR to make sure that only one index contains the character, glad my CS degree is coming in handy 
                if(password[positionOne] == character ^ password[positionTwo] == character)
                    validPasswords++;
            }

            Console.WriteLine("Part 2: {0}", validPasswords);
        }
    }
}
