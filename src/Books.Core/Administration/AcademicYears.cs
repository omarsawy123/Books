using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Books.Administration
{
    [Table("AcademicYears")]
    public class AcademicYears:FullAuditedEntity
    {
        public string AcademicYearName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  bool IsActive { get; set; }
        public  bool IsCurrent { get; set; }



    }
}
