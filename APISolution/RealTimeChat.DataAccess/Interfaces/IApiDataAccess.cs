using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface IApiDataAccess
{
    Task<List<ApplicationUser>> GetAllUsers();
}