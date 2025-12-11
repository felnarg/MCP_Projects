namespace Domain.Interfaces;

public interface IRepository
{
    public Task AddUser(UserModel user);
    public Task UpdateUser(UserModel user);
    public Task DeleteUser(Guid idUser);
    public Task<IEnumerable<UserModel>> GetUsers(string id);
}
