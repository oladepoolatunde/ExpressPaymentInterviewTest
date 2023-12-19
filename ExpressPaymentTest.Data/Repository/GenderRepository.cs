using ExpressPaymentTest.Data.IRepository;
using ExpressPaymentTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Data.Repository
{
    public class GenderRepository : GenericRepository<Gender>, IGenderRepository
    {
        public GenderRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
