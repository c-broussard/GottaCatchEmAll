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
    public class TypeRelations
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("damage_relations")]
        public DamageRelations DamageRelations { get; set; }
    }

    //public class DamageRelations
    //{
    //    [JsonPropertyName("no_damage_to")]
    //    public NoDamageToTypes[] NoDamageToTypes { get; set; }
    //    [JsonPropertyName("half_damage_to")]
    //    public HalfDamageToTypes[] HalfDamageToTypes { get; set; }
    //    [JsonPropertyName("double_damage_to")]
    //    public DoubleDamageToTypes[] DoubleDamageToTypes { get; set; }
    //    [JsonPropertyName("no_damage_from")]
    //    public NoDamageFromTypes[] NoDamageFromTypes { get; set; }
    //    [JsonPropertyName("half_damage_from")]
    //    public HalfDamageFromTypes[] HalfDamageFromTypes { get; set; }
    //    [JsonPropertyName("double_damage_from")]
    //    public DoubleDamageFromTypes[] DoubleDamageFromTypes { get; set; }
    //}

    public class DamageRelations
    {
        [JsonPropertyName("no_damage_to")]
        public DamageTypes[] NoDamageToTypes { get; set; }
        [JsonPropertyName("half_damage_to")]
        public DamageTypes[] HalfDamageToTypes { get; set; }
        [JsonPropertyName("double_damage_to")]
        public DamageTypes[] DoubleDamageToTypes { get; set; }
        [JsonPropertyName("no_damage_from")]
        public DamageTypes[] NoDamageFromTypes { get; set; }
        [JsonPropertyName("half_damage_from")]
        public DamageTypes[] HalfDamageFromTypes { get; set; }
        [JsonPropertyName("double_damage_from")]
        public DamageTypes[] DoubleDamageFromTypes { get; set; }
    }

    public class DamageTypes
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    //public class NoDamageToTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}

    //public class HalfDamageToTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}

    //public class DoubleDamageToTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}

    //public class NoDamageFromTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}

    //public class HalfDamageFromTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}

    //public class DoubleDamageFromTypes
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }
    //}
}
