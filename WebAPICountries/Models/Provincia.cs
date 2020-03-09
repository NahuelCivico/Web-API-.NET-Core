using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICountries.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Pais")]
        public int PaisId { get; set; }
        [JsonIgnore]
        public Pais Pais { get; set; }
    }
}
