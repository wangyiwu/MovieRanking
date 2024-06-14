using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryIsoCode { get; set; }
        public string CountryName { get; set; }

        public ICollection<ProductionCountry> ProductionCountries { get; set; }
    }
}
