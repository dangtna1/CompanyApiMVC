using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace companyApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company,GetCompanyDto>();
            CreateMap<AddCompanyDto,Company>();
        }
    }
}