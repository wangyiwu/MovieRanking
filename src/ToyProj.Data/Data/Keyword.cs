using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Keyword
    {
        [Key]
        public int KeywordId { get; set; }
        public string KeywordName { get; set; }

        public ICollection<MovieKeywords> MovieKeywords { get; set; }
    }
}
