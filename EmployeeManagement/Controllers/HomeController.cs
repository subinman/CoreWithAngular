using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    //[Route("Home")]
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //[Route("/")]
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Index")]
        public ViewResult Index()
        {
           // retrieve all the employees
        var model = _employeeRepository.GetAllEmployees();
            // Pass the list of employees to the view
            return View(model);
        }
        //[Route("Home/Details/{id?}")]
        //[Route("Details/{id?}")]
        public ViewResult Details(int? id)

        {
            // Instantiate HomeDetailsViewModel and store Employee details and PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };

            // Pass the ViewModel object to the View() helper method
            return View(homeDetailsViewModel);
        }
        public string Edit()
        {
            return "";
        }
       [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        //public RedirectToActionResult Create(Employee employee)
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }
        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return Json(model);
        //}

        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return new ObjectResult(model);
        //}
    }
}