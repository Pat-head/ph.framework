using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatHead.Framework.Repository;
using PatHead.Framework.Uow;
using WebApplicationTest.Domain;
using WebApplicationTest.Domain.Entities;
using WebApplicationTest.Persistence;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DemoEntity> _repository;
        private readonly IDemoRepository _demoRepository;

        public WeatherForecastController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.GetUnitOfWork();
            _repository = _unitOfWork.GetSimpleRepository<DemoEntity>();
            _demoRepository = _unitOfWork.GetRepository<IDemoRepository>();
        }


        [HttpGet]
        public async Task<string> Get()
        {
            var demoEntity = new DemoEntity();

            _repository.Add(demoEntity);

            await _unitOfWork.CommitAsync();

            var entity = demoEntity;
            
            demoEntity = await _demoRepository.GetQueryable().Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            demoEntity.Id = 11;

            _demoRepository.Update(demoEntity);

            await _unitOfWork.CommitAsync();

            _repository.Remove(demoEntity);

            await _unitOfWork.CommitAsync();

            return "";
        }
    }
}