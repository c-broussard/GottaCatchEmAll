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
        //Returns list of pokemon type(s) for the pokemon that is entered
        internal static async Task<List<PokemonType>> GetPokemonTypes(HttpClient client, string baseURL, string incomingPokemon)
        {
            var url = baseURL + string.Format("/pokemon/{0}", incomingPokemon.ToLower()); 

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
            
        }

        //Returns all type relations for the list of types provided
        internal static async Task GetTypeRelations(HttpClient client, List<PokemonType> pokemonTypes)
        {
            List<string> strongTypes = new List<string>();
            List<string> weakTypes = new List<string>();

            //Get the type relations for each pokemon type
            for (int i = 0; i < pokemonTypes.Count; i++)
            {
                await using Stream stream = 
                    await client.GetStreamAsync(pokemonTypes[i].Url);

                var typeRelations = 
                    await JsonSerializer.DeserializeAsync<TypeRelations>(stream);

                //Consider each damage type relations for damage to and damage from
                foreach (var property in typeRelations.DamageRelations.GetType().GetProperties())
                {
                    object[] propertyValues = property.GetValue(typeRelations.DamageRelations, null) as object[];

                    object currentObj = null;

                    
                    for (int j = 0; j < propertyValues.Length; j++)
                    {
                        currentObj = propertyValues[j].GetType().GetProperty("Name").GetValue(propertyValues[j], null);
                        
                        switch (property.Name)
                        {
                            case "NoDamageToTypes":
                            case "HalfDamageToTypes":
                            case "DoubleDamageFromTypes":
                                weakTypes.Add(currentObj.ToString());
                                break;
                            case "DoubleDamageToTypes":
                            case "NoDamageFromTypes":
                            case "HalfDamageFromTypes":
                                strongTypes.Add(currentObj.ToString());
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
                
            }
            
        }
    }
}
