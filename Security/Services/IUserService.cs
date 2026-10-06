using DataAccess.Entities;
using Security.Dtos;

namespace Security.Services;

public interface IUserService
{
    bool TryLogin(TryLogin user);
    
    void TryRegister(CreateUserRequest user);
}