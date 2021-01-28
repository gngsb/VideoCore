using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;

namespace VideoManage.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<UserModel, WUserinfo>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<HouseModel, WHouseinfo>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
