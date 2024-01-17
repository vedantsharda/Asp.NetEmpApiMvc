using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewEmpProject.Data;
using NewEmpProject.Models;

namespace NewEmpProject.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EmpApiController : ControllerBase
{
    private readonly IUserData _userData;

    public EmpApiController(IUserData userData)
    {
        _userData = userData;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employee = await _userData.GetUsers();
        return Ok(employee);
    }

    [HttpGet("{empId}")]
    public async Task<IActionResult> Get(int empId)
    {
        var employee = await _userData.GetUser(empId);
        if (employee is null)
            return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserModel user)
    {
        await _userData.InsertUser(user);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(UserModel user)
    {
        await _userData.UpdateUser(user);
        return Ok();
    }

    [HttpDelete("{EmpID}")]
    public async Task<IActionResult> Delete(int EmpID)
    {
        await _userData.DeleteUser(EmpID);
        return Ok();
    }

}