using AutoMapper;
using ExpressPaymentTest.Domain.Entities;
using ExpressPaymentTest.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Services.Settings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<RegistrationModel, User>().ReverseMap();
            


        }
    }
}
