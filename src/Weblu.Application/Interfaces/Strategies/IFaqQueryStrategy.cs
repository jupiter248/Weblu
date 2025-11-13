using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IFaqQueryStrategy
    {
        IQueryable<Faq> Query(IQueryable<Faq> faqs, FaqParameters faqParameters);
    }
}