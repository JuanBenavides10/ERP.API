using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests.Configuration;
using ERP.Accounting.Application.DTOs.Responses.Configuration;
using ERP.Accounting.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Mappings.Configuration
{  
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CreateCompanyRequest, CompanyEntity>();
            CreateMap<CompanyEntity,GetCompanyResponse>();
        }
    }
}
