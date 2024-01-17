using System;
using NewEmpProject.Models;

namespace NewEmpProject.Data;

public interface IUserData
{
    Task DeleteUser(int empId);
    Task<UserModel?> GetUser(int empId);
    Task<IEnumerable<UserModel>> GetUsers();
    Task InsertUser(UserModel user);
    Task UpdateUser(UserModel user);
}

