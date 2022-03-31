using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class StudentAllBooksDto:StudentsDto
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public List<SelectedBooksDto> SelectedBooks  { get; set; }

        public int SelectedBooksCount { get; set; }

        public decimal SelectedBooksTotal { get; set; }
        public string AcademicYearName { get;set; }
    }
}
