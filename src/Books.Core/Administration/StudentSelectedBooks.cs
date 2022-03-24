using Abp.Domain.Entities.Auditing;
using Books.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration
{
    [Table("StudentSelectedBooks")]
    public class StudentSelectedBooks : FullAuditedEntity
    {
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Students Student { get; set; }

        public IList<StudentBooks> Books { get; set; }
    }
}
