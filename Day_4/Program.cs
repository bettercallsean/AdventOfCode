using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dictionary<string, string>> passports = PassportParser("../../../passports.txt");
            ValidPassportChecker(passports);
        }

        static List<Dictionary<string, string>> PassportParser(string filename)
        {
            List<Dictionary<string, string>> passportData = new List<Dictionary<string, string>>();


            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                string passport = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        string[] splitPassport = passport.Split();
                        passportData.Add(splitPassport.ToDictionary(s => s.Split(":")[0], s => s.Split(":")[1]));
                        passport = "";
                    }

                    else
                    {
                        if (string.IsNullOrWhiteSpace(passport))
                            passport += line;
                        else
                            passport += " " + line;
                    }
                }
            }

            return passportData;
        }

        // Part 1
        static void ValidPassportChecker(List<Dictionary<string, string>> passportData)
        {
            int validPassportCount = 0;
            foreach(Dictionary<string, string> passport in passportData)
            {

                if (passport.Count >= 7)
                {
                    List<string> requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                    bool validPassport = true;

                    foreach (string field in requiredFields)
                    {
                        if (!passport.ContainsKey(field))
                        {
                            validPassport = false;
                            break;
                        }

                    }

                    if (validPassport)
                    {
                        bool validFields = true;
                        foreach (KeyValuePair<string, string> keyValuePair in passport)
                        {
                            if (!ValidPassportFieldData(keyValuePair))
                            {
                                validFields = false;
                                break;
                            }
                        }

                        if(validFields)
                            validPassportCount++;
                    }
                        
                }
            }

            Console.WriteLine("Part 1: {0}", validPassportCount);
        }

        static bool ValidPassportFieldData(KeyValuePair<string, string> passportField)
        {
            bool validField = false;
            switch (passportField.Key)
            {
                case "byr":
                    int.TryParse(passportField.Value, out int dob);
                    if (1920 <= dob && dob <= 2002)
                        validField = true;
                    break;

                case "iyr":
                    int.TryParse(passportField.Value, out int year);
                    if (2010 <= year && year <= 2020)
                        validField = true;
                    break;

                case "eyr":
                    int.TryParse(passportField.Value, out int expirationDate);
                    if (2020 <= expirationDate && expirationDate <= 2030)
                        validField = true;
                    break;

                case "hgt":
                    string heightString = passportField.Value[0..^2];
                    int.TryParse(heightString, out int height);
                    string unit = passportField.Value[heightString.Length..];

                    if (unit == "cm")
                        validField = (150 <= height && height <= 193);
                    else if (unit == "in")
                        validField = (59 <= height && height <= 76);
                    else
                        validField = false;
                    break;

                case "hcl":
                    if (passportField.Value[0] == '#' && passportField.Value.Length == 7)
                    {
                        string hairColour = passportField.Value[1..];
                        char[] validCharacters = { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2',
                        '3', '4', '5', '6', '7', '8', '9'};
                        foreach (char c in hairColour)
                        {
                            if (validCharacters.Contains(c))
                                validField = true;
                            else
                            {
                                validField = false;
                                break;
                            }
                        }
                    }
                    break;

                case "ecl":
                    string[] validEyeColours = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    if (validEyeColours.Contains(passportField.Value))
                        validField = true;
                    break;

                case "pid":
                    if (passportField.Value.Length == 9)
                        validField = true;
                    break;

                case "cid":
                    validField = true;
                    break;
            }
            
            return validField;
        }
    }
}
