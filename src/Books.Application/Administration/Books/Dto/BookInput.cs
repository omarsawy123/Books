using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Books.Dto
{
    public class BookInput: PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public int gradId { get; set; }

    }
}
