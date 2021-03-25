﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {

        private InternshipService _internshipService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _internshipService = new InternshipService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            _internshipService.Members.RemoveAt(index);
        }

        [HttpGet]
        public string AddMember(string member)
        {
            _internshipService.Members.Add(member);
            return member;
        }

        public IActionResult Privacy()
        {
            return View(_internshipService);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
