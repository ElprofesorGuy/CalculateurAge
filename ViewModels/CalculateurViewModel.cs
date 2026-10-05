namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today;
    private string _resultat = "";
    private bool _resultatVisible;

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
                CalculerCommand.Rafraichir();
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public RelayCommand CalculerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom)
                   && DateNaissance <= DateTime.Today);
    }

    private void Calculer()
    {
        DateTime aujourdHui = DateTime.Today;

        int age = aujourdHui.Year - DateNaissance.Year;

        if (DateNaissance.Date > aujourdHui.AddYears(-age))
        {
            age--;
        }

        string statut = age >= 18 ? "Vous êtes majeur." : "Vous êtes mineur.";

        Resultat =
            $"{Nom}, vous avez {age} ans.\n{statut}";

        ResultatVisible = true;
    }
}