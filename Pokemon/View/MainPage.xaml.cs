using Pokemon.ViewModels;

namespace Pokemon;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new PokemonViewModel();
    }

}

