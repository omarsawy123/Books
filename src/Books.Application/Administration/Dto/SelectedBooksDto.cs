using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class SelectedBooksDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsPrevious { get; set; }
        public string PublihserName { get; set; }
        public string BookGradeName { get; set; }
        public int BookGradeId { get; set; }
        public int StudentId { get; set; }

    }
}
