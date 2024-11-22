namespace Lab3;
public partial class EditPage : ContentPage
{
    private List<Book> books;
    public EditPage(List<Book> books)
    {
        InitializeComponent();
        this.books = books;
        TitlePicker.ItemsSource = books.Select(book => book.Title).Distinct().ToList();
    }

    private void AddBookButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(titleEntry.Text) || string.IsNullOrEmpty(authorEntry.Text))
        {
            DisplayAlert("Увага!", "Заповніть обов'язкові поля: Назва та Автор, щоб додати книгу", "OK");
            return;
        }

        var newBook = new Book
        {
            Title = titleEntry.Text,
            Author = authorEntry.Text,
            YearOfRelease = yearEntry.Text,
            Edition = editionEntry.Text,
            Genre = genreEntry.Text
        };

        books.Add(newBook);

        JsonFileManger.SaveBooksToFile(books);
        ClearEntry();   

        DisplayAlert("Успіх!", "Книга додана до списку.", "OK");

        TitlePicker.ItemsSource = books.Select(book => book.Title).Distinct().ToList();
    }

    private void EditBookButtonClicked(object sender, EventArgs e)
    {
        var selectedTitle = titleEntry.Text;

        if (string.IsNullOrEmpty(selectedTitle))
        {
            DisplayAlert("Увага!", "Щоб зберегти відредаговану книгу, спочатку здійсніть пошук та внесіть зміни", "OK");
            return;
        }

        var bookToEdit = books.FirstOrDefault(book => book.Title == selectedTitle);

        if (bookToEdit != null)
        {
            bookToEdit.Author = authorEntry.Text;
            bookToEdit.YearOfRelease = yearEntry.Text;
            bookToEdit.Edition = editionEntry.Text;
            bookToEdit.Genre = genreEntry.Text;

            JsonFileManger.SaveBooksToFile(books);

            titleEntry.Text = authorEntry.Text = yearEntry.Text = editionEntry.Text = genreEntry.Text = string.Empty;

            DisplayAlert("Успіх!", "Інформація про книгу відредагована.", "OK");
        }
        else
        {
            DisplayAlert("Помилка", "Книгу з такою назвою не знайдено.", "OK");
        }
    }

    private void SearchButtonClicked(object sender, EventArgs e)
    {
        var selectedTitle = TitlePicker.SelectedItem as string;

        if (string.IsNullOrEmpty(selectedTitle))
        {
            DisplayAlert("Увага!", "Оберіть хоча б один критерій перед пошуком.", "OK");
            return;
        }

        var bookToDisplay = books.FirstOrDefault(book => book.Title == selectedTitle);

        if (bookToDisplay != null)
        {
            titleEntry.Text = bookToDisplay.Title;
            authorEntry.Text = bookToDisplay.Author;
            yearEntry.Text = bookToDisplay.YearOfRelease;
            editionEntry.Text = bookToDisplay.Edition;
            genreEntry.Text = bookToDisplay.Genre;
        }
    }

    private void ClearButtonClicked(object sender, EventArgs e)
    {
        TitlePicker.SelectedItem = null;
        ClearEntry();
    }

    private void ClearEntry()
    {
        titleEntry.Text = string.Empty;
        authorEntry.Text = string.Empty;
        yearEntry.Text = string.Empty;
        editionEntry.Text = string.Empty;
        genreEntry.Text = string.Empty;
    }
    private async void MainPageClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}