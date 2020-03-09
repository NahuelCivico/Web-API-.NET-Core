using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICountries.Models
{
    public class Pais
    {
        public int Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; }
        public List<Provincia> Provincias { get; set; }

        public Pais()
        {
            Provincias = new List<Provincia>();
        }
    }
}
