using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Parameters.Common
{
    public class SearchParameters : BaseParameters
    {
        public SearchEntityType SearchEntityType { get; set; }
    }
}