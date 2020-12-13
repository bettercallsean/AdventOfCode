using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            Console.WriteLine("Part 2: {0}", validPassportCount);
        }

        // Part 2
        static bool ValidPassportFieldData(KeyValuePair<string, string> passportField)
        {
            string data = passportField.Value;
            switch (passportField.Key)
            {
                case "byr":
                    return Regex.IsMatch(data, "19[2-9][0-9]|200[0-2]");

                case "iyr":
                    return Regex.IsMatch(data, "20(1[0-9]|20)");

                case "eyr":
                    return Regex.IsMatch(data, "20(2[0-9]|30)");

                case "hgt":
                    return Regex.IsMatch(data, "1([5-8][0-9]|9[0-3])cm") || Regex.IsMatch(data, "5[3-9]|6[0-9]|7[0-6]in");

                case "hcl":
                    return Regex.IsMatch(data, "#[0-9a-f]{6}");

                case "ecl":
                    string[] validEyeColours = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    return validEyeColours.Contains(data);

                case "pid":
                    return data.Length == 9;

                case "cid":
                    return true;

                default:
                    return false;
            }
            
        }
    }
}
