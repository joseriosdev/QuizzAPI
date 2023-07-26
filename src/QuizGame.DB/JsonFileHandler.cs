using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace QuizGame.DB
{
    internal static class JsonFileHandler<T>
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public static async Task SaveJson(string nameFile, T modelObject)
        {
            using (FileStream fs = new FileStream(nameFile, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<T>(fs, modelObject, options);
            }
        }

        public static async Task<T> ReadJson(string nameFile)
        {
            using (FileStream fs = new FileStream(nameFile, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<T>(fs, options);
            }
        }

        public static async Task WriteJson(string nameFile, T modelObject)
        {
            using (FileStream fs = new FileStream(nameFile, FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync<T>(fs, modelObject, options);
            }
        }
    }
}
