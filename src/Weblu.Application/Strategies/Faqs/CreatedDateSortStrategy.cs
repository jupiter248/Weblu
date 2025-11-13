using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Faqs
{
    public class CreatedDateSortStrategy : IFaqQueryStrategy
    {
        public IQueryable<Faq> Query(IQueryable<Faq> faqs, FaqParameters faqParameters)
        {
            if (faqParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return faqs.OrderByDescending(c => c.CreatedAt);
            }
            else if (faqParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return faqs.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return faqs;
            }
        }
    }
}