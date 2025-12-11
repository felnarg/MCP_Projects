using MCP_Server_Users.ViewModels;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCP_Server_Users.Tools;

[McpServerToolType]
public class UserTools(
        HttpClient httpClient
    )
{
    private readonly HttpClient _httpClient = httpClient;

    [McpServerTool, Description("Obtiene el o los usuarios (UserVm) buscando por su correo electrónico, nombre completo (nombre) o número de telefono")]
    public async Task<List<UserVm>> GetUsers(
        [Description("Puede ser un nombre, apellido, número de telefono o correo electrónico")] string id 
        )
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://host.docker.internal:3030/getUsers?id={id}");

            response.EnsureSuccessStatusCode();

            var usersList = await response.Content.ReadFromJsonAsync<List<UserVm>>();

            return usersList!.ToList(); 
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        } 
    }

    [McpServerTool, Description("Elimina uno o varios usuarios por medio de su correo electrónico, nombre o número de telefono")]
    public async Task<string> DeleteUser(
        [Description("id tipo guid del usuario a eliminar")] Guid id
        )
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://host.docker.internal:3030/deleteUser?id={id}");

            response.EnsureSuccessStatusCode();

            return "El usuario ha sido eliminado.";
        }
        catch  
        {
            throw new Exception("No se pudo eliminar el id solicitado.");
        }
    }

    [McpServerTool, Description("Actualiza el usuario buscando por su correo electrónico, nombre o número de telefono")]
    public async Task<string> UpdateUsers(
        [Description("El id tipo Guid del usuario a actualizar en base de datos")] Guid userId,
        [Description("Nombre del usuario")] string name,
        [Description("Apellido del usuario")] string lastName,
        [Description("Email del usuario")] string email,
        [Description("Número de teléfono del usuario")] string phoneNumber
        )
    {
        try
        {
            var body = new UserVm
            {
                UserId = userId,
                Name = name,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            var response = await _httpClient.PutAsJsonAsync("http://host.docker.internal:3030/updateUser", body);

            response.EnsureSuccessStatusCode();

            return "El usuario ha sido actualizado.";
        }
        catch 
        {
            throw new Exception("No pudo actualizarse el usuario solicitado.");
        }
    }

    [McpServerTool, Description("Agrega un usuario a la base de datos")]
    public async Task<string> AddUser(
        [Description("El id tipo Guid del usuario a actualizar en base de datos")] Guid userId,
        [Description("Nombre del usuario")] string name,
        [Description("Apellido del usuario")] string lastName,
        [Description("Email del usuario")] string email,
        [Description("Número de teléfono del usuario")] string phoneNumber
        )
    {
        try
        {
            var body = new UserVm
            {
                UserId = userId,
                Name = name,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            var response = await _httpClient.PostAsJsonAsync($"http://host.docker.internal:3030/createUser", body);

            response.EnsureSuccessStatusCode();

            return "El usuario ha sido creado.";
        }
        catch
        {
            throw new Exception("No se pudo crear el usuario en base de datos.");
        }
    }

}
