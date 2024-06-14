using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class MovieCrew
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public int DepartmentId { get; set; }
        public string Job { get; set; }

        public Movie Movie { get; set; }
        public Person Person { get; set; }
        public Department Department { get; set; }
    }
}
