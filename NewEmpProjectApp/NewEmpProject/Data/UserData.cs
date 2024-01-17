using System;
using NewEmpProject.DataAccess;
using NewEmpProject.Models;

namespace NewEmpProject.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.GetEmployeeList", new { });

    public async Task<UserModel?> GetUser(int empId)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "dbo.GetEmployeeList1",
            new { EmpID = empId });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.InsertEmployee", new { user.EmpID, user.EmpName, user.EmpSalary, user.EmpAddress1,
            user.EmpAddress2, user.EmpAddress3, user.EmpState, user.District, user.Pincode });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("dbo.UpdateEmployee", user);

    public Task DeleteUser(int empId) =>
        _db.SaveData("dbo.DeleteEmployee", new { EmpID = empId });
}

