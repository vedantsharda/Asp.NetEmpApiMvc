using System;
namespace NewEmpProject.Models;

public class UserModel
{
    public int EmpID { get; set; }
    public string EmpName { get; set; }
    public string EmpSalary { get; set; }
    public string EmpAddress1 { get; set; }
    public string EmpAddress2 { get; set; }
    public string EmpAddress3 { get; set; }
    public string EmpState { get; set; }
    public string District { get; set; }
    public string Pincode { get; set; }
}