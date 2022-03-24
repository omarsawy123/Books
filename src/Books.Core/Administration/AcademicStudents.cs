using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("AcademicStudents")]
    public class AcademicStudents : FullAuditedEntity
    {

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Students Student { get; set; }

        [ForeignKey("AcademicGradeClasses")]
        public int AcademicGradeClassId { get; set; }
        public AcademicGradeClasses AcademicGradeClasses { get; set; }


    }
}
