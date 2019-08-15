using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using InterviewTestTemplatev2.ViewModels;
using InterviewTestTemplatev2.Data;

namespace InterviewTestTemplatev2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<HrEmployee, HrEmployeeConciseViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(source => source.ID))
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(source => source.Full_Name));
        }
    }
}