using AutoMapper;
using clean_architecture.Data;

public abstract class BaseHandler
{
    protected readonly AppDbContext _dbContext;
    protected readonly IMapper _mapper;

    protected BaseHandler(AppDbContext dbContext , IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
}