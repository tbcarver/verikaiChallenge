using System;
using System.Collections.Generic;

namespace verikai
{
    public static class GridWithHeaderConverter
    {
        public static List<Dictionary<string, string>> ConvertToHeaderAsKeyDictionaries(List<string[]> gridWithHeader)
        {
            List<Dictionary<string, string>> dictionaries = new List<Dictionary<string, string>>();

            if (gridWithHeader.Count > 0)
            {
                string[] header = gridWithHeader[0];

                if (gridWithHeader.Count > 1)
                {
                    for (int rowIndex = 1; rowIndex < gridWithHeader.Count; rowIndex++)
                    {
                        string[] row = gridWithHeader[rowIndex];

                        if (header.Length == row.Length)
                        {
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();

                            for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                            {
                                dictionary[header[columnIndex]] = row[columnIndex];
                            }

                            dictionaries.Add(dictionary);
                        }
                        else
                        {
                            Console.WriteLine("The raw data does not have the correct number of columns.");
                        }

                    }
                }
            }

            return dictionaries;
        }

        public static Dictionary<string, string> ConvertToFirstColumnAsKeyDictionary(List<string[]> gridWithHeader)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (gridWithHeader.Count > 1)
            {
                for (int rowIndex = 1; rowIndex < gridWithHeader.Count; rowIndex++)
                {
                    string[] row = gridWithHeader[rowIndex];

                    if (row.Length == 2)
                    {
                        dictionary[row[0]] = row[1];
                    }
                    else
                    {
                        Console.WriteLine("The raw data does not have the correct number of columns.");
                    }

                }
            }

            return dictionary;
        }
    }
}