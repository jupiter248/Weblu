using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Favorites.Parameters;

namespace Weblu.Application.Parameters
{
    public class FavoriteListParameters : BaseParameters
    {
        public FavoriteListTypeSort FavoriteListTypeSort { get; set; }
    }
}