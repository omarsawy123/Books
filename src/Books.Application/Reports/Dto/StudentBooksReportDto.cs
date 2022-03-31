using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Administration.Dto; 

namespace Books.Reports.Dto
{
    public class StudentBooksReportDto
    {
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string GradeName { get; set; }
        public string ClassName { get; set; }
        public string AcademicYearName { get; set; }

        public decimal BooksTotal { get; set; }

        public List<SelectedBooksDto> SelectedBooks { get; set; }



    }
}
