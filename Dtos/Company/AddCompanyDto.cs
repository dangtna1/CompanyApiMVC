using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace companyApi.Dtos.Company
{
    public class AddCompanyDto
    {
        public string? Name { get; set; }   
        public bool isActive { get; set; }
        public CompanyRating Rating { get; set; } = CompanyRating.FiveStar;
    }
}