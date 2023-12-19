using AutoMapper;
using ExpressPaymentTest.Data.IRepository;
using ExpressPaymentTest.Domain.Entities;
using ExpressPaymentTest.Services.DTO;
using ExpressPaymentTest.Services.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
        }

       
    }
}
