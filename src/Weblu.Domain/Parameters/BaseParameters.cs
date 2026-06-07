using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Common.Parameters
{
    public class BaseParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public virtual CreatedDateSort CreatedDateSort { get; set; }
    }
}