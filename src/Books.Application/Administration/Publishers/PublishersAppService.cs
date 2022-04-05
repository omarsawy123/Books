using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Books.Administration.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.PublisherAppService
{
    public class PublishersAppService : AsyncCrudAppService<Publishers,PublisherDto>
    {

        private readonly IRepository<Publishers, int> _puslishers;

        public PublishersAppService(IRepository<Publishers, int> puslishers) : base(puslishers)
        {
            _puslishers = puslishers;
        }

       

    }
}
