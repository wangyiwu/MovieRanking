using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string PersonName { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; }
        public ICollection<MovieCrew> MovieCrews { get; set; }
    }
}
