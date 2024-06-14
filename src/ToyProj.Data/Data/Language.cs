using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public ICollection<MovieLanguages> MovieLanguages { get; set; }
    }
}
