using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("AcademicGradeBooks")]
    public class AcademicGradeBooks : FullAuditedEntity
    {
        [ForeignKey("AcademicYear")]
        public int AcademicYearId { get; set; }
        public AcademicYears AcademicYear { get; set; }

        [ForeignKey("Grade")]
        public int GradeId { get; set; }
        public Grades Grade { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public StudentBooks Book { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publishers Publisher { get; set; }


    }
}
