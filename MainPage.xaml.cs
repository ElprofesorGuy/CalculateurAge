namespace CalculateurAge;

using CalculateurAge.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        string nom = entryNom.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nom))
        {
            lblResultat.Text = "Veuillez entrer votre nom.";
            lblResultat.IsVisible = true;
            return;
        }

        DateTime dateNaissance = pickerDate.Date;
        DateTime aujourdHui = DateTime.Today;

        int age = aujourdHui.Year - dateNaissance.Year;

        if (dateNaissance.Date > aujourdHui.AddYears(-age))
        {
            age--;
        }

        await Shell.Current.GoToAsync(
            $"{nameof(ResultatPage)}?nom={entryNom.Text}&age={age}");
    }
}