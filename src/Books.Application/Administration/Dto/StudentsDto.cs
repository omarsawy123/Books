using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Books.Administration;
using Books.Authorization.Users;

namespace Books.Administration.Dto
{
    [AutoMap(typeof(Students))]
    public class StudentsDto:EntityDto
    {
        public string StudentCode { get; set; }
        public string Name { get; set; }

        public string FamilyName { get; set; }
        public string NameAr { get; set; }
        public string FamilyNameAr { get; set; }

        public long? UserId { get; set; }

       
    }
}
