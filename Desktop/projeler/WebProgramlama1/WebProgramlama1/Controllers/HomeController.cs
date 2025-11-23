using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebProgramlama1.Models;

namespace WebProgramlama1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Index6()
    {
        ViewBag.deger1 = "Calismanin devami";
        return View();
    }
[HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult IslemDeneme(int a)
    {
        if (ModelState.IsValid)
        {
            
        }
        return View();
    }
}