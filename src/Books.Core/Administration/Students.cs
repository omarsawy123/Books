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
    [Table("Students")]
    public class Students: FullAuditedEntity
    {
        public string StudentCode { get; set; }
        public string Name { get; set; }
    
        public string FamilyName { get; set; }
        public string NameAr { get; set; }
        public string FamilyNameAr { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }

        public User User { get; set; }


    }
}
