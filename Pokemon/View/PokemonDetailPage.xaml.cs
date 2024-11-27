namespace Pokemon;
using Pokemon.Models;
using System.Windows.Input;

public partial class PokemonDetailPage : ContentPage
{
    public PokemonDetailPage(Poke selectedPokemon)
    {
        InitializeComponent();
        BindingContext = selectedPokemon;
    }
    private async void OnCloseClicked(object sender, EventArgs e)
    {
        // Naviga alla MainPage tramite la rotta
        await Shell.Current.GoToAsync("///MainPage");
    }
}