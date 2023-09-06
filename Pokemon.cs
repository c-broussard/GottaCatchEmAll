using System;
using System.Collections.Generic;
using System.Linq;
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
}