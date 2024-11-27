namespace Pokemon.Models;

public class Poke
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public int Height { get; set; } // Height in decimeters
    public int Weight { get; set; } // Weight in hectograms
    public List<PokemonTypeSlot> Types { get; set; }
    public List<PokemonAbilitySlot> Abilities { get; set; }
    public List<PokemonMove> Moves { get; set; }
    public List<PokemonStat> Stats { get; set; }
    public List<PokemonStat> PointStat { get; set; }
}

public class PokemonApiResponse
{
    public List<PokemonResult> Results { get; set; }
    public string Next { get; set; }
}

public class PokemonResult
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class PokemonTypeSlot
{
    public PokemonType Type { get; set; }
}

public class PokemonType
{
    public string Name { get; set; }
}

public class PokemonAbilitySlot
{
    public PokemonAbility Ability { get; set; }
}

public class PokemonAbility
{
    public string Name { get; set; }
}

public class PokemonMove
{
    public PokemonMoveDetails Move { get; set; }
}

public class PokemonMoveDetails
{
    public string Name { get; set; }
}

public class PokemonStat
{
    public PokemonStatDetails Stat { get; set; }
    public int BaseStat { get; set; }
}

public class PokemonStatDetails
{
    public string Name { get; set; }
}