using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class MovieKeywords
    {
        public int MovieId { get; set; }
        public int KeywordId { get; set; }

        public Movie Movie { get; set; }
        public Keyword Keyword { get; set; }
    }
}
