using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Administration
{
    [Table("StudentBooks")]
    public class StudentBooks:FullAuditedEntity
    {
        public string Name { get; set; }
        public string Isbn { get; set; }

        public int SortOrder { get; set; }

        public string Remarks { get; set; }
        public bool IsPreviousYear { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsAdditional { get; set; }

        public decimal Price { get; set; }


        //[ForeignKey("AcademicGradeBook")]
        //public int AcademicGradeBookId { get; set; }
        //public AcademicGradeBooks AcademicGradeBook { get; set; }


    }
}
