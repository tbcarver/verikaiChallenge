using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace verikai
{
    public static class GridWithHeaderReader
    {
        public static async Task<List<string[]>> GetHttpGridWithHeader(string url, string delimiter)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://devchallenge.verikai.com/data.tsv");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            string[] responseData = responseBody.Split("\n");
            List<string[]> grid = ConvertRowsToGrid(responseData, delimiter);

            return grid;
        }

        public static List<string[]> GetFileGridWithHeader(string filePath, string delimiter)
        {
            string[] rows = File.ReadAllLines(filePath);
            List<string[]> grid = ConvertRowsToGrid(rows, delimiter);

            return grid;
        }

        private static List<string[]> ConvertRowsToGrid(string[] rows, string delimiter)
        {
            List<string[]> grid = new List<string[]>();

            foreach (string row in rows)
            {
                string[] items = row.Split(delimiter);
                grid.Add(items);
            }

            return grid;
        }
    }
}
