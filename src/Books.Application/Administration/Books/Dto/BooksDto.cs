using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace Books.Administration.Books.Dto
{
    [AutoMap(typeof(StudentBooks))]
    public class BooksDto:EntityDto
    {
        public string Name { get; set; }
        public string ISBN { get; set; }

        public decimal Price { get; set; }
        public int BookGradeId { get; set; }
        public string BookGradeName { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsPrevoius { get; set; }
        public bool IsAdditional { get; set; }
        public string PublisherName { get; set; }

    }
}
