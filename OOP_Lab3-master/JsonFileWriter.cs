using System.Text.Json;

namespace Lab3
{
    public class JsonFileWriter
    {
        public static void SaveBooksToFile(string filePath, List<Book> books)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            string serializedBooks = JsonSerializer.Serialize(books, options);
            File.WriteAllText(filePath, serializedBooks);
        }
    }
}
