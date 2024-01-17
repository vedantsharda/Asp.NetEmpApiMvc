using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApiConsume.Models;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ApiConsume.Controllers;

public class HomeController : Controller
{
    //private readonly ILogger<HomeController> _logger;
    //string baseUrl = "https://localhost:7285/";

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}
    Uri baseAddress = new Uri("https://localhost:7285/api");
    private readonly HttpClient _client;
    public HomeController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<EmpViewModel> empList = new List<EmpViewModel>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/EmpApi/Get").Result;
        if (response.IsSuccessStatusCode)
        {
            string result = response.Content.ReadAsStringAsync().Result;
            empList = JsonConvert.DeserializeObject<List<EmpViewModel>>(result);
            
        }
        return View(empList);
    }

    [HttpGet]
    public async Task<IActionResult> Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Privacy(EmpViewModel model)
    {
        string data = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/EmpApi/Post", content).Result;
        if (response.IsSuccessStatusCode)
        {

            return RedirectToAction("Index");
        }
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int EmpID)
    {
        EmpViewModel model = new EmpViewModel();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/EmpApi/Get/" + EmpID).Result;
        if(response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<EmpViewModel>(data);
        }
        return View(model);
    }
        
    [HttpPost,ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int EmpID)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/EmpApi/Delete/" + EmpID).Result;
        if(response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Edit(int EmpID)
    {
        EmpViewModel model = new EmpViewModel();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/EmpApi/Get/" + EmpID).Result;
        if(response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<EmpViewModel>(data);
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(EmpViewModel model)
    {
        string data = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/EmpApi/Put", content).Result;
        if(response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View();
    }








    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

