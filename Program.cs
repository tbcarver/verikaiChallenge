using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace verikai
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Dictionary<string, Dictionary<string, string>> lookupData = new Dictionary<string, Dictionary<string, string>>();

            List<string[]> fileGrid = GridWithHeaderReader.GetFileGridWithHeader(@"Data\zip3-to-class.csv", ",");
            lookupData["zip3-to-class"] = GridWithHeaderConverter.ConvertToFirstColumnAsKeyDictionary(fileGrid);

            fileGrid = GridWithHeaderReader.GetFileGridWithHeader(@"Data\class-to-cost.csv", ",");
            lookupData["class-to-cost"] = GridWithHeaderConverter.ConvertToFirstColumnAsKeyDictionary(fileGrid);

            List<string[]> rawPeopleGrid = await GridWithHeaderReader.GetHttpGridWithHeader("http://devchallenge.verikai.com/data.tsv", "\t");
            List<Dictionary<string, string>> peopleDictionaries = GridWithHeaderConverter.ConvertToHeaderAsKeyDictionaries(rawPeopleGrid);

            RawPersonProcessor.ProcessRawPeople(peopleDictionaries, lookupData);

            Directory.CreateDirectory("ProcessedPeople");
            DictionaryWithHeaderWriter.WriteFileGridWithHeader(@"ProcessedPeople\unencrypted.tsv", "\t", peopleDictionaries);
            Console.WriteLine("File created: unencrypted.tsv");

            AesFileEncryptor fileEncryptor = new AesFileEncryptor("AXe8YwuIn1zxt3FPWTZFlAa14EHdPAdN9FaZ9RQWihc=", "bsxnWolsAyO7kCfWuyrnqg==");
            fileEncryptor.EncryptFile(@"ProcessedPeople\unencrypted.tsv", @"ProcessedPeople\encrypted.txt");
            Console.WriteLine("File created: encrypted.txt");
        }
    }
}
