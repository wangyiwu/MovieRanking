using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Column("Gender")]
        public string GenderName { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; }
    }
}
