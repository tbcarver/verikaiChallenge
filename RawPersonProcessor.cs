using System;
using System.Collections.Generic;
using System.Text;

namespace verikai
{
    public static class RawPersonProcessor
    {
        public static void ProcessRawPeople(List<Dictionary<string, string>> rawPeople, Dictionary<string, Dictionary<string, string>> lookupData)
        {
            foreach (Dictionary<string, string> rawPerson in rawPeople)
            {
                ProcessRawPerson(rawPerson, lookupData);
            }
        }

        public static void ProcessRawPerson(Dictionary<string, string> rawPerson, Dictionary<string, Dictionary<string, string>> lookupData)
        {
            string key = "gender";
            string gender;
            if (rawPerson.TryGetValue(key, out gender))
            {
                if (gender.StartsWith("M"))
                {
                    rawPerson[key] = "M";
                }
                else if (gender.StartsWith("F"))
                {
                    rawPerson[key] = "F";
                }
                else
                {
                    Console.WriteLine($"The {key} '{gender}' is invalid.");
                }
            }

            key = "dob";
            string dob;
            DateTime dobDate = DateTime.MinValue;
            if (rawPerson.TryGetValue(key, out dob))
            {
                dobDate = DateTime.Parse(dob);

                if (dobDate < DateTime.Now)
                {
                    rawPerson[key] = dobDate.ToShortDateString();
                }
                else
                {
                    rawPerson[key] = "";
                    dobDate = DateTime.MinValue;
                    Console.WriteLine($"Invalid dob '${dob}'");
                }
            }

            key = "phone";
            string phone;
            if (rawPerson.TryGetValue(key, out phone))
            {
                StringBuilder phoneBuilder = new StringBuilder();
                for (int index = phone.Length - 1; index >= 0; index--)
                {
                    char phoneCharacter = phone[index];
                    if (Char.IsNumber(phoneCharacter))
                    {
                        phoneBuilder.Insert(0, phoneCharacter);
                    }

                    if (phoneBuilder.Length == 10)
                    {
                        break;
                    }
                }

                if (phoneBuilder.Length == 10)
                {
                    rawPerson[key] = phoneBuilder.ToString();
                }
                else
                {
                    rawPerson[key] = "";
                    Console.WriteLine($"The {key} '{phone}' is invalid.");
                }
            }

            key = "age";
            rawPerson[key] = "";
            if (dobDate != DateTime.MinValue)
            {
                TimeSpan ageTimeSpan = DateTime.Now - dobDate;
                int years = (new DateTime(1, 1, 1) + ageTimeSpan).Year - 1;
                rawPerson[key] = years.ToString();
            }

            key = "cost";
            rawPerson[key] = "";
            string zip;
            if (rawPerson.TryGetValue("zip", out zip))
            {
                if (zip.Length >= 3)
                {
                    zip = zip.Substring(0, 3);
                    string zip3Class;
                    if (lookupData["zip3-to-class"].TryGetValue(zip, out zip3Class))
                    {
                        string cost;
                        if (lookupData["class-to-cost"].TryGetValue(zip3Class, out cost))
                        {
                            rawPerson[key] = cost;
                        }
                    }
                }
            }
        }
    }
}