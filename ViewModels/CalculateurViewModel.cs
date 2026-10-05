namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private string _prochainAnniversaire = "";
    private DateTime _dateNaissance = DateTime.Today;
    private string _resultat = "";
    private bool _resultatVisible;
    private string _generation = "";

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public string ProchainAnniversaire
    {
        get => _prochainAnniversaire;
        set => SetField(ref _prochainAnniversaire, value);
    }

    public string Generation
    {
        get => _generation;
        set => SetField(ref _generation, value);
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

        DateTime prochainAnniversaire = new DateTime(
            aujourdHui.Year,
            DateNaissance.Month,
            DateNaissance.Day);

        if (prochainAnniversaire < aujourdHui)
        {
            prochainAnniversaire = prochainAnniversaire.AddYears(1);
        }

        int joursRestants =
            (prochainAnniversaire - aujourdHui).Days;

        ProchainAnniversaire =
            $"Votre prochain anniversaire est dans {joursRestants} jour(s).";

        if (age < 13)
        {
            Generation = "Vous êtes un enfant.";
        }
        else if (age < 18)
        {
            Generation = "Vous êtes un adolescent.";
        }
        else if (age < 60)
        {
            Generation = "Vous êtes un adulte.";
        }
        else
        {
            Generation = "Vous êtes un senior.";
        }
    }
}