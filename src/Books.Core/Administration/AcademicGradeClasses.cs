using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("AcademicGradeClasses")]
    public class AcademicGradeClasses : FullAuditedEntity
    {
        [ForeignKey("AcademicYear")]
        public int? AcademicYearId { get; set; }
        public AcademicYears AcademicYear { get; set; }

        [ForeignKey("Grade")]
        public int? GradeId { get; set; }
        public Grades Grade { get; set; }

        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public Classes Class { get; set; }
        public IList<AcademicStudents> AcademicStudents { get; set; }
    }
}
