using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class StudentInputDto: PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public int gradeId { get; set; }

    }
}
