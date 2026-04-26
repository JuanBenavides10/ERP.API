using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.DTOs.Responses;
using ERP.Accounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERP.Accounting.Application.Mappings
{
    public class AnexosProfile : Profile
    {
        public AnexosProfile()
        {
            CreateMap<CreateAnexosRequest, AnexosEntity>();
            CreateMap<UpdateAnexosRequest, AnexosEntity>();
            CreateMap<AnexosEntity, GetAnexosResponse>();
        }

    }
}
