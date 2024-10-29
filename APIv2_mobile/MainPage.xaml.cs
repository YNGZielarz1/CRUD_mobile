using System.Collections.ObjectModel;

namespace APIv2_mobile
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<User> users; // Kolekcja do przechowywania danych użytkowników

        public MainPage()
        {
            InitializeComponent();
            users = new ObservableCollection<User>();
            usersCollectionView.ItemsSource = users; // Przypisz kolekcję jako źródło dla CollectionView
        }

        // Dodaj nowego użytkownika
        private void OnAddUserClicked(object sender, EventArgs e)
        {
            if (int.TryParse(idEntry.Text, out int id) && !string.IsNullOrEmpty(nameEntry.Text))
            {
                var newUser = new User
                {
                    Id = id,
                    Name = nameEntry.Text,
                    Location = locationEntry.Text,
                    Place = placeEntry.Text,
                    ResponsiblePerson = responsiblePersonEntry.Text
                };
                users.Add(newUser);
                DisplayAlert("Sukces", "Użytkownik został dodany", "OK");
            }
            else
            {
                DisplayAlert("Błąd", "ID oraz nazwa są wymagane.", "OK");
            }
        }

        // Wyświetl wszystkich użytkowników
        private void OnGetUsersClicked(object sender, EventArgs e)
        {
            // Przypisz listę użytkowników do CollectionView, aby je wyświetlić
            usersCollectionView.ItemsSource = users;
            DisplayAlert("Informacja", $"Łącznie użytkowników: {users.Count}", "OK");
        }

        // Wyświetl szczegóły użytkownika
        private void OnGetUserClicked(object sender, EventArgs e)
        {
            if (int.TryParse(idEntry.Text, out int id))
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    // Wyświetlanie danych użytkownika
                    userIdLabel.Text = $"ID: {user.Id}";
                    userNameLabel.Text = $"Nazwa: {user.Name}";
                    userPlaceLabel.Text = $"Miejsce: {user.Place}";
                    userLocationLabel.Text = $"Lokalizacja: {user.Location}";
                    userResponsiblePersonLabel.Text = $"Osoba odpowiedzialna: {user.ResponsiblePerson}";

                    userDetailsFrame.IsVisible = true; // Pokazujemy szczegóły
                }
                else
                {
                    DisplayAlert("Błąd", "Nie znaleziono użytkownika o podanym ID", "OK");
                    userDetailsFrame.IsVisible = false;
                }
            }
            else
            {
                DisplayAlert("Błąd", "Wprowadź poprawny numer ID", "OK");
            }
        }

        // Edytuj użytkownika
        private void OnEditUserClicked(object sender, EventArgs e)
        {
            if (int.TryParse(idEntry.Text, out int id))
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Name = nameEntry.Text;
                    user.Place = placeEntry.Text;
                    user.Location = locationEntry.Text;
                    user.ResponsiblePerson = responsiblePersonEntry.Text;
                    DisplayAlert("Sukces", "Użytkownik został zaktualizowany", "OK");
                }
                else
                {
                    DisplayAlert("Błąd", "Nie znaleziono użytkownika o podanym ID", "OK");
                }
            }
            else
            {
                DisplayAlert("Błąd", "Wprowadź poprawny numer ID", "OK");
            }
        }

        // Usuń użytkownika
        private void OnDeleteUserClicked(object sender, EventArgs e)
        {
            if (int.TryParse(idEntry.Text, out int id))
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    users.Remove(user);
                    DisplayAlert("Sukces", "Użytkownik został usunięty", "OK");
                    userDetailsFrame.IsVisible = false;
                }
                else
                {
                    DisplayAlert("Błąd", "Nie znaleziono użytkownika o podanym ID", "OK");
                }
            }
            else
            {
                DisplayAlert("Błąd", "Wprowadź poprawny numer ID", "OK");
            }
        }
    }

    // Klasa reprezentująca użytkownika
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Place { get; set; }
        public string ResponsiblePerson { get; set; }
    }
}
