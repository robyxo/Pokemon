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
            //Convert the response into json and assign it data. I used dynamic for the complexity of the json.
            var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var types = new List<string> { "None" }; // Default option
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

            // Iterate through the list of Pokémon for that type
            foreach (var pokemon in data.pokemon)
            {
                var name = pokemon.pokemon.name.ToString();
                var url = pokemon.pokemon.url.ToString();

                // Extract ID from URL
                var parts = url.Split('/');
                var id = parts[parts.Length - 2];
                // Convert ID to integer
                var pokemonId = int.Parse(id);

                // Add Pokémon to the list
                pokemons.Add(new Poke
                {
                    Name = name,
                    Id = pokemonId,
                    ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemonId}.png" // Image URL
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
                // Let's create a Poke object
                var pokemon = new Poke
                {
                    Id = id,
                    Name = data.name,
                    ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
                    Height = data.height / 10.0, // Convert from decimeters to meters
                    Weight = data.weight / 10.0, // Convert from hectograms to kg
                    Types = new List<PokemonTypeSlot>(),
                    Abilities = new List<PokemonAbilitySlot>(),
                    Moves = new List<PokemonMove>(),
                    Stats = new List<PokemonStat>()
                };

                // Adds types (PokemonTypeSlot)
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

                // Adds abilities (PokemonAbilitySlot)
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

                // Adds moves (PokemonMove)
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

                // Adds stats (PokemonStat)
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
