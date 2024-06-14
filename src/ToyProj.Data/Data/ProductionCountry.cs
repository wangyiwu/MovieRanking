using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class ProductionCountry
    {
        public int MovieId { get; set; }
        public int CountryId { get; set; }

        public Movie Movie { get; set; }
        public Country Country { get; set; }
    }
}
