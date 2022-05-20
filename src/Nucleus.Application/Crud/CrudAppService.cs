using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nucleus.Application.Dto;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Collections.Extensions;
using System.Linq.Dynamic.Core;

namespace Nucleus.Application.Crud;

public abstract class CrudAppService<TEntity, TGetOutputDto, TGetListOutput, TCreateInput, TUpdateInput>
    : ICrudAppService<TEntity, TGetOutputDto, TGetListOutput, TCreateInput, TUpdateInput>
    where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    protected CrudAppService(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public virtual async Task<TGetOutputDto> GetAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);

        return _mapper.Map<TGetOutputDto>(entity);
    }

    public virtual async Task<IPagedListResult<TGetListOutput>> GetListAsync(PagedListInput input)
    {
        IQueryable<TEntity> query;
        if (input.Filters == null || input.Filters.Count == 0)
        {
            query = _dbContext.Set<TEntity>();
        }
        else
        {
            var predicate = string.Join(" && ", input.Filters);
            query = _dbContext.Set<TEntity>().Where(predicate);
        }

        var orderedQuery = query.OrderBy(string.Join(',', input.Sorts));
        var count = await orderedQuery.CountAsync();
        var pagedList = orderedQuery.PagedBy(input.PageIndex, input.PageSize);
        var pagedListOutput = _mapper.Map<List<TGetListOutput>>(pagedList);

        return pagedListOutput.ToPagedListResult(count);
    }

    public virtual async Task<TGetOutputDto> CreateAsync(TCreateInput input)
    {
        var entity = _mapper.Map<TEntity>(input);
        var result = await _dbContext.AddAsync(entity);

        return _mapper.Map<TGetOutputDto>(result.Entity);
    }

    public virtual TGetOutputDto Update(TUpdateInput input)
    {
        var entity = _mapper.Map<TEntity>(input);
        var result = _dbContext.Update(entity);

        return _mapper.Map<TGetOutputDto>(result.Entity);
    }

    public virtual async Task<TGetOutputDto> DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        var result = _dbContext.Remove(entity);

        return _mapper.Map<TGetOutputDto>(result.Entity);
    }
}