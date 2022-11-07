# PatHead.Framework.Uow.EFCore

## 简单开始

```cs
// 通过在Startup.cs 中 ConfigureServices配置模块中进行配置

services.AddUnitOfWork<EFCoreUnitOfWorkFactory>(options =>
{
    // 注册管理数据库上下文给予UnitOfWork进行管理。
    options.RegisterManagementContext = new List<Type>()
    {
        typeof(TestDbContext)
    };
});
```

```cs
private readonly IUnitOfWork _unitOfWork;
private readonly IRepository<DemoEntity> _demoRepository;

public WeatherForecastController(IUnitOfWorkFactory unitOfWorkFactory)
{
    _unitOfWork = unitOfWorkFactory.GetUnitOfWork();
    _demoRepository = _unitOfWork.GetSimpleRepository<DemoEntity>();
}

[HttpGet]
public async Task<string> Get()
{
    var demoEntity = new DemoEntity();
    
    _demoRepository.Add(demoEntity);
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
```

