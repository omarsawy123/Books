using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Administration;

namespace Books.Administration.Dto
{
    [AutoMap(typeof(Publishers))]
    public class PublisherDto:EntityDto
    {
        public string Name { get; set; }
    }
}
