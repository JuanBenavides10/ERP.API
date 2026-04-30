using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Responses.Company;
using ERP.Accounting.Domain.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Mappings.Company
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyEntity, GetCompanyResponse>();

            CreateMap<CreateCompanyRequest, CompanyEntity>()
                        .ForMember(d => d.LogoPath, opt => opt.Ignore());

            CreateMap<UpdateCompanyRequest, CompanyEntity>()
                        // Evita que mapper cambie el Uuid, Id o campos no editables
                        .ForMember(d => d.Uuid, opt => opt.Ignore())
                        .ForMember(d => d.Id, opt => opt.Ignore())
                        .ForMember(d => d.Code, opt => opt.Ignore())
                        .ForMember(d => d.LogoPath, opt => opt.Ignore());

            CreateMap<CompanyEntity, GetAllCompanyResponse>();

        }
    }
}
