using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class FaqCategoryRepository : GenericRepository<FaqCategory , FaqCategoryParameters>, IFaqCategoryRepository
    {
        public FaqCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}