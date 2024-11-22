using System.Text.Json;

namespace Lab3
{
    public class JsonFileManger
    {
        private static string openedFilePath;

        public static string OpenedFilePath
        {
            get => openedFilePath;
            set => openedFilePath = value;
        }
        public static async Task<List<Book>> ReadBooksFromFile()
        {
            if (OpenedFilePath == null)
            {
                throw new ArgumentNullException(nameof(OpenedFilePath));
            }

            return await JsonFileReader.ReadBooksFromFile(OpenedFilePath);
        }
        public static void SaveBooksToFile(List<Book> books)
        {
            if (OpenedFilePath == null)
            {
                throw new ArgumentNullException(nameof(OpenedFilePath));
            }

            JsonFileWriter.SaveBooksToFile(OpenedFilePath, books);
        }
    }
}
