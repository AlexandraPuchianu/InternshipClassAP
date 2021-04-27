using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipClass.Controllers
{
    [Route("employee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbService employeeDbService;

        public EmployeeController(EmployeeDbService employeeDbService)
        {
            this.employeeDbService = employeeDbService;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeDbService.GetEmployees();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeDbService.GetEmployeeById(id);
        }

        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            employeeDbService.AddEmployee(employee);
        }
    }
}
