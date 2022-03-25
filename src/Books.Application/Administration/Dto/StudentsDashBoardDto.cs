using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class StudentsDashBoardDto:EntityDto
    {

        public string GradeName { get; set; }
        public int RegisteredStudents  { get; set; }
        public int  UnRegisteredStudents { get; set; }
        public int TotalStudents { get; set; }

    }
}
