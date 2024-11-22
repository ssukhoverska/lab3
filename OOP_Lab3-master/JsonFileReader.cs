using System.Text.Json;

namespace Lab3
{
    public class JsonFileReader
    {
        public static async Task<List<Book>> ReadBooksFromFile(string filePath)
        {
            string jsonString = await File.ReadAllTextAsync(filePath);
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            if (books != null)
            {
                return books;
            }
            else
            {
                return new List<Book>();
            }
        }
    }
}
