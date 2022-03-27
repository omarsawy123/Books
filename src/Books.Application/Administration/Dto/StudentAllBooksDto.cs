using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class StudentAllBooksDto:StudentsDto
    {
        public List<SelectedBooksDto> SelectedBooks  { get; set; }
    }
}
