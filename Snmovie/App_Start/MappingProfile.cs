using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Snmovie.Dtos;
using Snmovie.Models;

namespace Snmovie.App_Start
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      Mapper.CreateMap<Customer, CustomerDto>();
      Mapper.CreateMap<Movie, MovieDto>();


      // Dto to Domain
      Mapper.CreateMap<Customer, CustomerDto>()
                .ForMember(c => c.Id, opt => opt.Ignore());

      Mapper.CreateMap<Movie, MovieDto>()
        .ForMember(m => m.Id, opt => opt.Ignore());

  
    }
  }
}
