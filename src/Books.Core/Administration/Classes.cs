using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("Classes")]
    public class Classes : FullAuditedEntity
    {
        public string Name { get; set; }

        public string NameAr { get; set; }

        [ForeignKey("Grade")]
        public int GradeId { get; set; }

        public Grades Grade { get; set; }

    }
}
