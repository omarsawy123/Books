using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("Publishers")]
    public class Publishers : FullAuditedEntity
    {
        public string Name { get; set; }
        public IList<StudentBooks> Books { get; set; }
    }
}
