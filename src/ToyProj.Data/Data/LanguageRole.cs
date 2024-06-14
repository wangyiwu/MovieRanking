using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class LanguageRoles
    {
        [Key]
        public int RoleId { get; set; }
        public string LanguageRole { get; set; }

        public ICollection<MovieLanguages> MovieLanguages { get; set; }
    }
}
