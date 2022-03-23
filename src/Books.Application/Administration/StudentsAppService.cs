using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Books.Administration;
using Books.Administration.Dto;

namespace Books.StudentsAppServices
{
    public  class StudentsAppService: AsyncCrudAppService<Students, StudentsDto>
    {

        public StudentsAppService(IRepository<Students> repository)
        : base(repository)
        {

        }
    }
}
