// See https://aka.ms/new-console-template for more information
using GottaCatchEmAll;
using System.Net.Http.Headers;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

var baseURL = "https://pokeapi.co/api/v2/";

Console.Write("Enter your Pokemon: ");

var pokemon = Console.ReadLine();


List<PokemonType> pokemonTypes = await GetData.GetPokemonTypes(client, baseURL, pokemon);

await GetData.GetTypeRelations(client, pokemonTypes);