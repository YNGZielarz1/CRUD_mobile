using System.Collections.ObjectModel;
using System.Linq;

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
            if (!string.IsNullOrEmpty(nameEntry.Text))
            {
                // Znajdź największe ID w kolekcji użytkowników i dodaj 1
                int newId = users.Any() ? users.Max(u => u.Id) + 1 : 1;

                var newUser = new User
                {
                    Id = newId,
                    Name = nameEntry.Text,
                    Location = locationEntry.Text,
                    Place = placeEntry.Text,
                    ResponsiblePerson = responsiblePersonEntry.Text
                };

                users.Add(newUser);
                DisplayAlert("Sukces", $"Użytkownik został dodany z ID: {newId}", "OK");

                // Wyczyść pola po dodaniu użytkownika
                ClearEntryFields();
            }
            else
            {
                DisplayAlert("Błąd", "Nazwa jest wymagana.", "OK");
            }
        }

        // Wyczyść pola wprowadzania
        private void ClearEntryFields()
        {
            nameEntry.Text = string.Empty;
            locationEntry.Text = string.Empty;
            placeEntry.Text = string.Empty;
            responsiblePersonEntry.Text = string.Empty;
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

        // Usuń użytkownika z widoku szczegółowego
        private void OnDeleteUserClickedFromDetails(object sender, EventArgs e)
        {
            if (int.TryParse(userIdLabel.Text.Split(':')[1].Trim(), out int id))
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
                DisplayAlert("Błąd", "Nie można usunąć użytkownika. Spróbuj ponownie.", "OK");
            }
        }

        // Wyświetl wszystkich użytkowników
        private void OnGetUsersClicked(object sender, EventArgs e)
        {
            // Przypisz listę użytkowników do CollectionView, aby je wyświetlić
            usersCollectionView.ItemsSource = users;
            DisplayAlert("Informacja", $"Łącznie użytkowników: {users.Count}", "OK");
        }
        // Wyświetl szczegóły użytkownika po kliknięciu na element w CollectionView
        private void OnUserSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = e.CurrentSelection.FirstOrDefault() as User;
            if (selectedUser != null)
            {
                // Ustaw szczegóły użytkownika
                userIdLabel.Text = $"ID: {selectedUser.Id}";
                userNameLabel.Text = $"Nazwa: {selectedUser.Name}";
                userPlaceLabel.Text = $"Miejsce: {selectedUser.Place}";
                userLocationLabel.Text = $"Lokalizacja: {selectedUser.Location}";
                userResponsiblePersonLabel.Text = $"Osoba odpowiedzialna: {selectedUser.ResponsiblePerson}";

                userDetailsFrame.IsVisible = true; // Pokaż szczegóły
                                                   // Zresetuj wybór, aby uniknąć ponownego wywołania zdarzenia
                usersCollectionView.SelectedItem = null;
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
