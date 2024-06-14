﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class ProductionCompany
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<MovieCompany> MovieCompanies { get; set; }
    }
}
