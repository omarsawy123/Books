using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("Grades")]
    public class Grades : FullAuditedEntity
    {
        public string Name { get; set; }

        public string NameAr { get; set; }

        public int StudyOrder { get; set; }

        public IList<Classes> Classes { get; set; }

        public IList<StudentBooks> Books { get; set; }
    }
}
