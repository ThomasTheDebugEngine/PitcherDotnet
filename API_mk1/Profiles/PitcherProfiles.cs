using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Dtos;
using API_mk1.Dtos.ProjectDtos;
using API_mk1.Dtos.User;
using API_mk1.Models.Project;
using API_mk1.Models.User;
using AutoMapper;

namespace API_mk1.Profiles
{
    public class PitcherProfile : Profile
    {
        public PitcherProfile()
        {
            CreateMap<UserModel, UserGetDto>();
            CreateMap<UserPostDto, UserModel>();

            CreateMap<ProjectModel, ProjectGetDto>();
            CreateMap<ProjectPostDto, ProjectModel>();
        }
    }
}
