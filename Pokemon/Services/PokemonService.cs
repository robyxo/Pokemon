using Newtonsoft.Json;
using Pokemon.Models;

namespace Pokemon.Services;

internal class PokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
    }

    public async Task<(List<Poke>, string)> GetPokemonsAsync(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<PokemonApiResponse>(await response.Content.ReadAsStringAsync());
                var pokemons = new List<Poke>();

                foreach (var result in data.Results)
                {
                    var id = int.Parse(result.Url.Split('/').Reverse().Skip(1).First());
                    pokemons.Add(new Poke
                    {
                        Name = result.Name,
                        Id = id,
                        ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png"
                    });
                }

                return (pokemons, data.Next);
            }
        }
        catch (Exception ex)
        {
            // Log the error or show a message to the user
            Console.WriteLine($"Error fetching data: {ex.Message}");
        }
        return (null, null);
    }

    public async Task<List<string>> GetPokemonTypesAsync()
    {
        var response = await _httpClient.GetAsync("type");
        if (response.IsSuccessStatusCode)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var types = new List<string> { "Nessuno" }; // "Nessuno" è l'opzione predefinita
            foreach (var type in data.results)
            {
                types.Add(type.name.ToString());
            }
            return types;
        }
        return null;
    }

    public async Task<List<Poke>> GetPokemonsByTypeAsync(string type)
    {
        var response = await _httpClient.GetAsync($"type/{type}");
        if (response.IsSuccessStatusCode)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var pokemons = new List<Poke>();

            // Itera attraverso la lista di Pokémon per quel tipo
            foreach (var pokemon in data.pokemon)
            {
                var name = pokemon.pokemon.name.ToString();
                var url = pokemon.pokemon.url.ToString();

                // Estrai l'ID dal URL
                var parts = url.Split('/');
                var id = parts[parts.Length - 2];
                var pokemonId = int.Parse(id); // Converti l'ID in un intero

                // Aggiungi il Pokémon alla lista
                pokemons.Add(new Poke
                {
                    Name = name,
                    Id = pokemonId,
                    ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemonId}.png" // URL dell'immagine
                });
            }

            return pokemons;
        }
        return null;
    }

    public async Task<Poke> GetPokemonDetailsAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"pokemon/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                // Creiamo un oggetto Poke
                var pokemon = new Poke
                {
                    Id = id,
                    Name = data.name,
                    ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
                    Height = data.height / 10.0, // Converti da decimetri a metri
                    Weight = data.weight / 10.0, // Converti da ettogrammi a kg
                    Types = new List<PokemonTypeSlot>(),
                    Abilities = new List<PokemonAbilitySlot>(),
                    Moves = new List<PokemonMove>(),
                    Stats = new List<PokemonStat>()
                };

                // Aggiungiamo i tipi (PokemonTypeSlot)
                foreach (var type in data.types)
                {
                    pokemon.Types.Add(new PokemonTypeSlot
                    {
                        Type = new PokemonType
                        {
                            Name = type.type.name
                        }
                    });
                }

                // Aggiungiamo le abilità (PokemonAbilitySlot)
                foreach (var ability in data.abilities)
                {
                    pokemon.Abilities.Add(new PokemonAbilitySlot
                    {
                        Ability = new PokemonAbility
                        {
                            Name = ability.ability.name
                        }
                    });
                }

                // Aggiungiamo le mosse (PokemonMove)
                foreach (var move in data.moves)
                {
                    pokemon.Moves.Add(new PokemonMove
                    {
                        Move = new PokemonMoveDetails
                        {
                            Name = move.move.name
                        }
                    });
                }

                // Aggiungiamo le statistiche (PokemonStat)
                foreach (var stat in data.stats)
                {
                    pokemon.Stats.Add(new PokemonStat
                    {
                        Stat = new PokemonStatDetails
                        {
                            Name = stat.stat.name
                        },
                        BaseStat = stat.base_stat
                    });
                }
                return pokemon;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching details: {ex.Message}");
        }
        return null;
    }
}
