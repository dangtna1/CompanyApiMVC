using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace companyApi.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public bool isActive { get; set; }
        public CompanyRating Rating { get; set; } = CompanyRating.FiveStar;
    }
}