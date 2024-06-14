using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{

    public class MovieCast
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public int GenderId { get; set; }
        public string CharacterName { get; set; }
        public int CastOrder { get; set; }

        public Movie Movie { get; set; }
        public Person Person { get; set; }
        public Gender Gender { get; set; }
    }
}
