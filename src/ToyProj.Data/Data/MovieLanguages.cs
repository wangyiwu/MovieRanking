using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class MovieLanguages
    {
        public int MovieId { get; set; }
        public int LanguageId { get; set; }
        public int LanguageRoleId { get; set; }

        public Movie Movie { get; set; }
        public Language Language { get; set; }
        public LanguageRoles LanguageRole { get; set; }
    }
}
