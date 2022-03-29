using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Books.Dto
{
    public class RequiredBooksDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Isbn { get; set; }
        public string PublisherName { get; set; }
        public string GradeName { get; set; }
        public int GradeId { get; set; }
        public int Count { get; set; }

        public bool IsMandatory { get; set; }




    }
}
