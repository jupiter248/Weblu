using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFaqCategoryRepository: IGenericRepository<FaqCategory, FaqCategoryParameters>
    {
    }
}