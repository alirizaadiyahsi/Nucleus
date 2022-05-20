using Nucleus.Application.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Crud;

public interface ICrudAppService<TEntity, TGetOutputDto, TGetListOutput, in TCreateInput, in TUpdateInput>
{
    Task<TGetOutputDto> GetAsync(Guid id);
    Task<IPagedListResult<TGetListOutput>> GetListAsync(PagedListInput input);
    Task<TGetOutputDto> CreateAsync(TCreateInput input);
    TGetOutputDto Update(TUpdateInput input);
    Task<TGetOutputDto> DeleteAsync(Guid id);
}