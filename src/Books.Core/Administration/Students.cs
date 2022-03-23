using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("Students")]
    public class Students: FullAuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
