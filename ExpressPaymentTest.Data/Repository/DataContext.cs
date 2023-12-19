using ExpressPaymentTest.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpressPaymentTest.Data.Repository
{
    public class DataContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
    }
}
