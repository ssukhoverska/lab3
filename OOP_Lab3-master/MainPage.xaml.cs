namespace Lab3
{
    public partial class MainPage : ContentPage
    {
        private List<Book> books;
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OpenJsonFileButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".json" } },
                });

                var options = new PickOptions { FileTypes = customFileType, };

                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    JsonFileManger.OpenedFilePath = result.FullPath;
                    books = await JsonFileManger.ReadBooksFromFile();

                    UpdatePickerItemsSources();

                    await DisplayAlert("Успіх!", "Файл успішно відкритий та оброблений.", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Помилка", "Не вдалося відкрити файл, спробуйте ще раз.", "OK");
            }

            BookDisplayManager.DisplayBooks(books, BookGrid);
        }
        private void ShowAllBooksButtonClicked(object sender, EventArgs e)
        {
            if (books == null)
            {
                DisplayAlert("Увага!", "Спочатку відкрийте файл.", "OK");
                return;
            }

            BookDisplayManager.DisplayBooks(books, BookGrid);
            UpdatePickerItemsSources();
        }
        private void UpdatePickerItemsSources()
        {
            AuthorPicker.ItemsSource = books.Select(book => book.Author).Distinct().ToList();
            YearPicker.ItemsSource = books.Where(book => book.YearOfRelease != null).Select(book => book.YearOfRelease).Distinct().ToList();
            GenrePicker.ItemsSource = books.Where(book => book.Genre != null).Select(book => book.Genre).Distinct().ToList();
        }
        private async void EditDataPageClicked(object sender, EventArgs e)
        {
            if (books == null)
            {
                await DisplayAlert("Увага!", "Відкрийте файл, щоб перейти на сторінку редактування.", "OK");
                return;
            }
            await Navigation.PushAsync(new EditPage(books));
        }
        private async void InfoPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var selectedAuthor = AuthorPicker.SelectedItem as string;
            var selectedYear = YearPicker.SelectedItem as string;
            var selectedGenre = GenrePicker.SelectedItem as string;

            if (selectedAuthor == null && selectedYear == null && selectedGenre == null)
            {
                await DisplayAlert("Увага!", "Знайдіть потрібну книгу, щоб видалити її.", "OK");
                return;
            }

            var bookToDelete = books.FirstOrDefault(book =>
                (string.IsNullOrEmpty(selectedAuthor) || book.Author == selectedAuthor) &&
                (string.IsNullOrEmpty(selectedYear) || book.YearOfRelease == selectedYear) &&
                (string.IsNullOrEmpty(selectedGenre) || book.Genre == selectedGenre)
            );

            if (bookToDelete != null)
            {
                bool answer = await DisplayAlert("Підтвердження", $"Ви дійсно хочете видалити книгу '{bookToDelete.Title}'?", "Так", "Ні");

                if (answer)
                {
                    int indexToRemove = books.IndexOf(bookToDelete);
                    books.RemoveAt(indexToRemove);

                    BookGrid.Children.Clear();

                    await DisplayAlert("Успіх!", "Книга успішно видалена.", "OK");
                    JsonFileManger.SaveBooksToFile(books);
                }
            }
            else
            {
                await DisplayAlert("Помилка", "Обраної книги не знайдено для видалення.", "OK");
            }
        }

        private void SearchButtonClicked(object sender, EventArgs e)
        {
            var selectedAuthor = AuthorPicker.SelectedItem as string;
            var selectedYear = YearPicker.SelectedItem as string;
            var selectedGenre = GenrePicker.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedAuthor) && string.IsNullOrEmpty(selectedYear) && string.IsNullOrEmpty(selectedGenre))
            {
                DisplayAlert("Увага!", "Оберіть хоча б один критерій перед пошуком.", "OK");
                return;
            }

            IEnumerable<Book> filteredBooks = books;

            if (!string.IsNullOrEmpty(selectedAuthor))
            {
                filteredBooks = filteredBooks.Where(book => book.Author == selectedAuthor);
            }

            if (!string.IsNullOrEmpty(selectedYear))
            {
                filteredBooks = filteredBooks.Where(book => book.YearOfRelease == selectedYear);
            }

            if (!string.IsNullOrEmpty(selectedGenre))
            {
                filteredBooks = filteredBooks.Where(book => book.Genre == selectedGenre);
            }

            DisplayFilteredBooks(filteredBooks.ToList());

        }
        private void DisplayFilteredBooks(List<Book> filteredBooks)
        {
            BookDisplayManager.DisplayBooks(filteredBooks, BookGrid);
        }
        private void ClearButtonClicked(object sender, EventArgs e)
        {
            BookGrid.Children.Clear();
            AuthorPicker.SelectedItem = null;
            YearPicker.SelectedItem = null;
            GenrePicker.SelectedItem = null;
        }
        private async void ExitButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти з програми?", "Так", "Ні");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }

    }
}