using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace GottaCatchEmAll
{
    internal class GetData
    {
        //Kickoffs the processes to retrieve pokemon data.
        internal static async Task<PokemonAttributes> GetPokemon()
        {

            //Initialize HttpClient (could be class or dependency injection)
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var baseURL = "https://pokeapi.co/api/v2/";

            List<PokemonType> pokemonTypes = new List<PokemonType>();

            //Prompts user to enter a pokemon.
            string pokemonName = "";
            do
            {
                Console.Write("Enter a Pokemon: ");

                pokemonName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(pokemonName) || pokemonName.Any(char.IsDigit))
                {
                    Console.WriteLine("Please enter a valid Pokemon name.");
                }
                pokemonTypes = await GetPokemonTypes(client, baseURL, pokemonName);

            } while (string.IsNullOrWhiteSpace(pokemonName) || pokemonName.Any(char.IsDigit) || pokemonTypes == null);

            

            //Gets data needed to build pokemon attributes. 
            //If pokemon is not found, exception is caught and user is notified.
            try {
                
                PokemonDamageTypes pokemonDamageTypes = await GetData.GetTypeRelations(client, pokemonTypes);
                PokemonAttributes pokemon = new PokemonAttributes(pokemonName, pokemonTypes, pokemonDamageTypes);
                Console.WriteLine("Pokemon found!!\n");
                return pokemon;
            }
            catch (Exception e)
            {
                Console.WriteLine("Pokemon was not found. Please enter a valid Pokemon name.");
                return null;
            }; 
        }

        //Returns list of pokemon type(s) for the pokemon that is entered.
        internal static async Task<List<PokemonType>> GetPokemonTypes(HttpClient client, string baseURL, string incomingPokemon)
        {
            var url = baseURL + string.Format("/pokemon/{0}", incomingPokemon.ToLower());


            try
            {
                await using Stream stream =
                            await client.GetStreamAsync(url);

                var pokemon =
                await JsonSerializer.DeserializeAsync<Pokemon>(stream);

                List<PokemonType> typesList = new List<PokemonType>();

                for (int i = 0; i < pokemon.Slots.Count; i++)
                {
                    typesList.Add(pokemon.Slots[i].PokemonType);
                }
                return typesList;

            } catch (Exception e)
            {
                Console.WriteLine("Pokemon was not found. Please enter a valid Pokemon name.");
                return null;
            }
            
            
            
            
        }

        //Returns all type damage relations for the list of types provided
        internal static async Task<PokemonDamageTypes> GetTypeRelations(HttpClient client, List<PokemonType> pokemonTypes)
        {
            PokemonDamageTypes pokemonDamageTypes = new PokemonDamageTypes();

            //Hashsets used to only include distinct values.
            HashSet<string> strongTypes = new HashSet<string>();
            HashSet<string> weakTypes = new HashSet<string>();

            //Get the type relations for each pokemon type
            for (int i = 0; i < pokemonTypes.Count; i++)
            {
                await using Stream stream = 
                    await client.GetStreamAsync(pokemonTypes[i].Url);

                var typeRelations = 
                    await JsonSerializer.DeserializeAsync<TypeRelations>(stream);

                
                //Considers each damage type relations for damage to and damage from.
                //Should be broken out into smaller tasks.
                foreach (var damageRelation in typeRelations.DamageRelations.GetType().GetProperties())
                {
                    DamageTypes[] damageRelationsValues = (DamageTypes[])damageRelation.GetValue(typeRelations.DamageRelations, null);

                    var currentType = "";

                    //Builds a list of weak types and strong types.
                    for (int j = 0; j < damageRelationsValues.Length; j++)
                    {
                        currentType = damageRelationsValues[j].GetType().GetProperty("Name").GetValue(damageRelationsValues[j], null).ToString();
                        
                        switch (damageRelation.Name)
                        {
                            case "NoDamageToTypes":
                            case "HalfDamageToTypes":
                            case "DoubleDamageFromTypes":
                                weakTypes.Add(currentType.ToString());
                                break;
                            case "DoubleDamageToTypes":
                            case "NoDamageFromTypes":
                            case "HalfDamageFromTypes":
                                strongTypes.Add(currentType.ToString());
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
            }
            pokemonDamageTypes.WeakTypes = weakTypes;
            pokemonDamageTypes.StrongTypes = strongTypes;

            return pokemonDamageTypes;

            
        }
    }
}
