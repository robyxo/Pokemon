using System.Collections.ObjectModel;
using System.Windows.Input;
using Pokemon.Services;
using Pokemon.Models;

namespace Pokemon.ViewModels;

internal class PokemonViewModel : BindableObject
{
    private readonly PokemonService _pokemonService;
    private string _nextUrl = "pokemon?offset=0&limit=20";
    private bool _isLoading;
    private List<string> _pokemonTypes;
    private string _selectedType;

    public ObservableCollection<Poke> Pokemons { get; } = new ObservableCollection<Poke>();

    public ICommand LoadMoreCommand { get; }
    public ICommand TypeChangedCommand { get; }
    public ICommand NavigateToDetailCommand { get; }

    public PokemonViewModel()
    {
        _pokemonService = new PokemonService();
        LoadMoreCommand = new Command(async () => await LoadMorePokemonsAsync());
        TypeChangedCommand = new Command(async () => await OnTypeChangedAsync());
        NavigateToDetailCommand = new Command<Poke>(async (pokemon) => await NavigateToDetailAsync(pokemon));
        Task.Run(async () => await LoadMorePokemonsAsync());
        Task.Run(async () => await LoadPokemonTypesAsync());
    }

    public List<string> PokemonTypes
    {
        get => _pokemonTypes;
        set
        {
            if (_pokemonTypes != value)
            {
                _pokemonTypes = value;
                OnPropertyChanged();
            }
        }
    }

    public string SelectedType
    {
        get => _selectedType;
        set
        {
            if (_selectedType != value)
            {
                _selectedType = value;
                OnPropertyChanged();
                // Quando il tipo cambia, ricarica la lista dei Pokémon
                Task.Run(async () => await OnTypeChangedAsync());
            }
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
    }

    private async Task NavigateToDetailAsync(Poke pokemon)
    {
        if (pokemon == null) return;

        // Ottieni i dettagli completi del Pokémon
        var details = await _pokemonService.GetPokemonDetailsAsync(pokemon.Id);
        if (details != null)
        {
            // Naviga verso la pagina di dettaglio con i dettagli completi
            await Application.Current.MainPage.Navigation.PushAsync(new PokemonDetailPage(details));
        }
    }

    private async Task LoadPokemonTypesAsync()
    {
        var types = await _pokemonService.GetPokemonTypesAsync();
        PokemonTypes = types;
    }

    private async Task OnTypeChangedAsync()
    {
        Pokemons.Clear();
        _nextUrl = null; // Resetta il URL per la paginazione

        // Verifica se il tipo selezionato è "Nessuno" o un tipo valido
        if (string.IsNullOrEmpty(SelectedType) || SelectedType == "Nessuno")
        {
            // Se "Nessuno" è selezionato, carica tutti i Pokémon con paginazione
            await LoadAllPokemonsAsync();
        }
        else
        {
            // Se un tipo specifico è selezionato, carica i Pokémon di quel tipo senza paginazione
            await LoadPokemonsByTypeAsync(SelectedType);
        }
    }

    private async Task LoadAllPokemonsAsync()
    {
        IsLoading = true;

        // Esegui la chiamata all'API per caricare i Pokémon
        var (allPokemons, nextUrl) = await _pokemonService.GetPokemonsAsync(_nextUrl ?? "pokemon?offset=0&limit=20");

        // Aggiorna la variabile _nextUrl con il valore della paginazione
        _nextUrl = nextUrl;

        if (allPokemons != null)
        {
            foreach (var pokemon in allPokemons)
                Pokemons.Add(pokemon);
        }
        IsLoading = false;
    }

    private async Task LoadPokemonsByTypeAsync(string type)
    {
        IsLoading = true;

        // Chiamata API per il tipo selezionato (senza paginazione)
        var pokemons = await _pokemonService.GetPokemonsByTypeAsync(type);

        if (pokemons != null)
        {
            foreach (var pokemon in pokemons)
                Pokemons.Add(pokemon);
        }
        IsLoading = false;
    }


    private async Task LoadMorePokemonsAsync()
    {
        if (IsLoading || _nextUrl == null)
            return;

        IsLoading = true;

        List<Poke> pokemons = null;

        if (string.IsNullOrEmpty(SelectedType) || SelectedType == "Nessuno")
        {
            var (allPokemons, nextUrl) = await _pokemonService.GetPokemonsAsync(_nextUrl);
            pokemons = allPokemons;
            _nextUrl = nextUrl;
        }
        else
        {
            pokemons = await _pokemonService.GetPokemonsByTypeAsync(SelectedType);
        }

        if (pokemons != null)
        {
            foreach (var pokemon in pokemons)
                Pokemons.Add(pokemon);
        }

        IsLoading = false;
    }
}
