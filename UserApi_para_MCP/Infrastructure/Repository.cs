using Domain;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Infrastructure;

public class Repository : IRepository
{
    private readonly UserContext _userContext;
    public Repository(UserContext userContext)
    {
        _userContext = userContext;
    }
    public async Task AddUser(UserModel user)
    {
        await _userContext.Set<UserModel>().AddAsync(user);
        await _userContext.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid idUser)
    {
        var idUserString = idUser.ToString();
        var user = await _userContext.Set<UserModel>().FirstOrDefaultAsync(user => user.UserId == idUser);
        if (user != null)
        {
            _userContext.Set<UserModel>().Remove(user);
            await _userContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("No se encontró el usuario y no pudo ser eliminado.");
        }
    }

    public async Task<IEnumerable<UserModel>> GetUsers(string id)
    {
        if (id.IsNullOrEmpty())
            return new List<UserModel>();

        var searchType = IdentifyIdType(id.Trim());
        var cleanId = id.Trim();

        IQueryable<UserModel> query = _userContext.Users.AsNoTracking();

        switch (searchType) 
        { 
            case IdType.Email: 
                query = query.Where(user => user.Email == cleanId); 
                break;
            case IdType.PhoneNumber:
                query = query.Where(user => user.PhoneNumber == cleanId); 
                break;
            case IdType.FullName:
                query = query.Where(user => user.Name!.Contains(cleanId) || user.LastName!.Contains(cleanId)).Distinct();
                break;
        }

        return await query.ToListAsync();
    }

    public async Task UpdateUser(UserModel user)
    {
        _userContext.Users.Update(user);
        await _userContext.SaveChangesAsync();
    }   

    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex PhoneRegex = new Regex(
        @"^[\d\s\-\+\(\).]{7,20}$",
        RegexOptions.Compiled);

    private IdType IdentifyIdType(string id)
    {
        if (EmailRegex.IsMatch(id))
        {
            return IdType.Email;
        }

        if (PhoneRegex.IsMatch(id))
        {
            return IdType.PhoneNumber;
        }

        return IdType.FullName;
    }
}
