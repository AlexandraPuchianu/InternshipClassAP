using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {

        private readonly InternshipService intershipService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, InternshipService internshipService)
        {
            this.intershipService = internshipService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetClass());
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public string AddMember(string member)
        {
            return intershipService.AddMember(member);
        }

        [HttpPut]
        public void UpdateMember(int index, string name)
        {
            intershipService.UpdateMember(index, name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
