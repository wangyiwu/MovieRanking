using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; }
    }
}
