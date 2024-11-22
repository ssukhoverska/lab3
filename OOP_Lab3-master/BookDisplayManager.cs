namespace Lab3
{
    public class BookDisplayManager
    {
        public static void DisplayBooks(List<Book> books, Grid bookGrid)
        {
            bookGrid.Children.Clear();
            if (books != null)
            {

                for (int i = 0; i < books.Count; i++)
                {
                    bookGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    var book = books[i];

                    var titleLabel = new Label { Text = book.Title, };

                    var authorLabel = new Label { Text = book.Author, };

                    var yearLabel = new Label { Text = book.YearOfRelease, };

                    var editionLabel = new Label { Text = book.Edition, };

                    var genreLabel = new Label { Text = book.Genre, };

                    Grid.SetRow(titleLabel, i);
                    Grid.SetColumn(titleLabel, 0);

                    Grid.SetRow(authorLabel, i);
                    Grid.SetColumn(authorLabel, 1);

                    Grid.SetRow(yearLabel, i);
                    Grid.SetColumn(yearLabel, 2);

                    Grid.SetRow(editionLabel, i);
                    Grid.SetColumn(editionLabel, 3);

                    Grid.SetRow(genreLabel, i);
                    Grid.SetColumn(genreLabel, 4);

                    bookGrid.Children.Add(titleLabel);
                    bookGrid.Children.Add(authorLabel);
                    bookGrid.Children.Add(yearLabel);
                    bookGrid.Children.Add(editionLabel);
                    bookGrid.Children.Add(genreLabel);
                }
            }
        }
    }
}
