using Nucleus.Application.Authorization.Users.Dto;
using Nucleus.Application.Crud;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Authorization.Users;

public interface IUserAppService : ICrudAppService<User, UserOutput, UserListOutput, CreateUserInput, UpdateUserInput>
{

}