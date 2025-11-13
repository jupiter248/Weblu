using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Strategies.Faqs
{
    public class FaqQueryHandler
    {
        private readonly IFaqQueryStrategy _faqQueryStrategy;
        public FaqQueryHandler(IFaqQueryStrategy faqQueryStrategy)
        {
            _faqQueryStrategy = faqQueryStrategy;
        }
        public IQueryable<Faq> ExecuteFaqQuery(IQueryable<Faq> faqs, FaqParameters faqParameters)
        {
            return _faqQueryStrategy.Query(faqs, faqParameters);
        }
    }
}