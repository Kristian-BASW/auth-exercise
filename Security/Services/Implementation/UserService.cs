using DataAccess.Entities;
using DataAccess.Repositories;
using Security.Dtos;
using Security.Services.Security;

namespace Security.Services.Implementation;

public class UserService(IUserRepository userRepo, IPasswordHasher passwordHasher) : IUserService
{
    public bool TryLogin(TryLogin user)
    {
        var dbUser = userRepo.GetUserByUsername(user.Username);
        if(dbUser == null)
        {
            return false;
        }
        return passwordHasher.VerifyPassword(user.Password, dbUser.PasswordHash);
    }

    public void TryRegister(CreateUserRequest user)
    {
        string passwordHash = passwordHasher.HashAndSaltPassword(user.Password);
        userRepo.CreateUser(new User
        {
            Username = user.Username,
            PasswordHash = passwordHash
        });
    }
}