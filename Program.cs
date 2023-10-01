// See https://aka.ms/new-console-template for more information
using GottaCatchEmAll;

PokemonAttributes pokemon = await GetData.GetPokemon();

if (pokemon != null)
{
    Console.WriteLine("Name: {0}", pokemon.Name.ToUpper());

    Console.Write("\nType: ");

    int i = 0;
    foreach (var type in pokemon.PokemonTypes)
    {

        if (pokemon.PokemonTypes.Count == 2 && i == 0)
        {
            Console.Write("{0}/", type.TypeName.ToString().ToUpper());
            i++;
        }
        else
        {
            Console.Write("{0}", type.TypeName.ToString().ToUpper());
        }

    }

    Console.Write("\n\nStrengths: ");

    foreach (var strong in pokemon.DamageTypes.StrongTypes)
    {
        Console.Write("{0} ", strong.ToString().ToUpper());
    }

    Console.Write("\n\nWeaknesses: ");

    foreach (var weak in pokemon.DamageTypes.WeakTypes)
    {
        Console.Write("{0} ", weak.ToString().ToUpper());
    }
} 


