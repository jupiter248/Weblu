using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}