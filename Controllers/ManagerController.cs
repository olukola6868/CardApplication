using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Services.Implementations;
using HarnyCardApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HarnyCardApplication.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateManagerRequestModel model)
        {
            if (model != null)
            {
                var create = await _managerService.Create(model);
                TempData["success"] = $"{model.FirstName} {model.LastName} Created Successfully";
                TempData.Keep();
                return RedirectToAction("Login", "User");
            }
            else
            {
                TempData["error"] = "Wrong Input";
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _managerService.Delete(id);
            ViewBag.Message = "Customer deleted successfully";
            return RedirectToAction();
        }
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _managerService.Get(id);
            if (customer.Status == true)
            {
                return View(customer.Data);
            }
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var customer = await _managerService.GetAll();
            if (customer.Status == true)
            {
                return View(customer.Data);
            }
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public async Task<IActionResult> Update(int id, UpdateManagerRequestModel model)
        {
            await _managerService.Update(id, model);
            ViewBag.Message = "Customer edited successfully";
            return RedirectToAction();
        }

        public IActionResult ManagerBoard()
        {
            return View();
        }
    }
}