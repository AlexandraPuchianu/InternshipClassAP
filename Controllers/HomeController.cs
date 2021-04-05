using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInternshipService intershipService;
        private readonly ILogger<HomeController> _logger;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, InternDbContext db)
        {
            this.intershipService = internshipService;
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            var interns = db.Interns.ToList();
            return View(interns);
        }

        public IActionResult Privacy()
        {
            var interns = intershipService.GetMembers();
            return View(interns);
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.DateOfJoin = DateTime.Now;

            return intershipService.AddMember(intern);
        }

        [HttpPut]
        public void UpdateMember(int id, string newName)
        {
            Intern intern = new Intern();
            intern.Id = id;
            intern.Name = newName;
            intershipService.UpdateMember(intern);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
