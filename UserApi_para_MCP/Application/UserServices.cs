using Application.Interfaces;
using Domain;
using Domain.Interfaces;

namespace Application;

public class UserServices(
    IRepository userRepository
    ) : IUserServices
{        
    private readonly IRepository _userRepository = userRepository;

    public async Task AddUser(UserModel user)
    {
        await _userRepository.AddUser(user);
    }

    public async Task DeleteUser(Guid idUser)
    {
        await _userRepository.DeleteUser(idUser);
    }

    public async Task<List<UserModel>> GetUsers (string id) 
    {
        var users = await _userRepository.GetUsers(id);
        return users.ToList();
    }

    public async Task UpdateUser(UserModel user)
    {
        await _userRepository.UpdateUser(user);
    }
}
