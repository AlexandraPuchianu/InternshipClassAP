using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Hubs;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInternshipService intershipService;
        private readonly ILogger<HomeController> _logger;
        private readonly MessageService messageService;
        private readonly IHubContext<MessageHub> hubContext;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            this.intershipService = internshipService;
            _logger = logger;
            this.messageService = messageService;
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View(intershipService.GetMembers());
        }

        public IActionResult Privacy()
        {
            var interns = intershipService.GetMembers();
            return View(interns);
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            var internsList = intershipService.GetMembers();
            Intern intern = internsList[index];

            intershipService.RemoveMember(intern.Id);
        }
        //Change to post
        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.DateOfJoin = DateTime.Now;

            var newMember = intershipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
            return newMember;
        }

        [HttpPut]
        public void UpdateMember(int index, string newName)
        {
            var internsList = intershipService.GetMembers();
            Intern intern = internsList[index];
            intern.Name = newName;
            intern.DateOfJoin = DateTime.Now;
            intershipService.UpdateMember(intern);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
