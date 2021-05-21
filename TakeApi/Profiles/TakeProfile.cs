using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeApi.Dtos;
using TakeApi.Model;

namespace TakeApi.Profiles
{
    public class TakeProfile : Profile
    {
        public TakeProfile()
        {
            int count = 0;
            CreateMap<RepositoriesDTO, Challenge>().AfterMap((src, dest) =>
            {    
                    dest.id = count++;
            }).ForMember(
                dest => dest.avatar_url, opt =>
                opt.MapFrom(src => src.owner.avatar_url)
                ); 
        }
    }
}
