using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Books.Administration;


namespace Books.Administration.Dto
{
    [AutoMap(typeof(Students))]
    public class StudentsDto:EntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
