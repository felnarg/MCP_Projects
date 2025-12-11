using Domain;

namespace Application.Interfaces;

public interface IUserServices
{
    public Task AddUser(UserModel user);
    public Task UpdateUser(UserModel user);
    public Task DeleteUser(Guid idUser);
    public Task<List<UserModel>> GetUsers(string id);
}

