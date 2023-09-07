using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GottaCatchEmAll
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("types")]
        public List<TypeSlot> Slots { get; set; }
    }

    public class TypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
        [JsonPropertyName("type")]
        public PokemonType PokemonType { get; set; }
    }

    public class PokemonType
    {
        [JsonPropertyName("name")]
        public string TypeName { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class PokemonDamageTypes
    {
        public HashSet<string> WeakTypes { get; set; }
        public HashSet<string> StrongTypes { get; set; }
    }

    public class PokemonAttributes
    {
        public string Name { get; set; }
        public List<PokemonType> PokemonTypes { get; set; }
        public PokemonDamageTypes DamageTypes { get; set; }
        public PokemonAttributes(string pokeName, List<PokemonType> pokeTypes, PokemonDamageTypes pokeDamageTypes)
        {
            Name = pokeName;
            PokemonTypes = pokeTypes;
            DamageTypes = pokeDamageTypes;
            
        }
    }
}