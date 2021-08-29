using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace verikai
{
    public static class DictionaryWithHeaderWriter
    {
        public static void WriteFileGridWithHeader(string pathName, string delimiter, List<Dictionary<string, string>> headerAsKeyDictionaries)
        {
            using (StreamWriter fileWriter = new StreamWriter(@"ProcessedPeople\unencrypted.tsv"))
            {
                if (headerAsKeyDictionaries.Count > 0)
                {
                    StringBuilder rowBuilder = new StringBuilder();
                    Dictionary<string, string> dictionary = headerAsKeyDictionaries[0];
                    List<string> keys = dictionary.Keys.ToList();

                    if (keys.Count > 0)
                    {
                        foreach (string key in keys)
                        {
                            rowBuilder.Append(key);
                            rowBuilder.Append(delimiter);
                        }

                        rowBuilder.Remove(rowBuilder.Length - 1, 1);
                        fileWriter.WriteLine(rowBuilder);

                        if (headerAsKeyDictionaries.Count > 1)
                        {
                            for (int index = 1; index < headerAsKeyDictionaries.Count; index++)
                            {
                                rowBuilder.Clear();
                                dictionary = headerAsKeyDictionaries[index];

                                foreach (string key in keys)
                                {
                                    rowBuilder.Append(dictionary[key]);
                                    rowBuilder.Append(delimiter);
                                }

                                rowBuilder.Remove(rowBuilder.Length - 1, 1);
                                fileWriter.WriteLine(rowBuilder);
                            }
                        }
                    }
                }
            }
        }
    }
}
