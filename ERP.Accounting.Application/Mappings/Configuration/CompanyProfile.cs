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


            CreateMap<UpdateCompanyRequest, CompanyEntity>()
                        // Evita que mapper cambie el Uuid, Id o campos no editables
                        .ForMember(d => d.Uuid, opt => opt.Ignore())
                        .ForMember(d => d.Id, opt => opt.Ignore())
                        .ForMember(d => d.Code, opt => opt.Ignore());

        }
    }
}
