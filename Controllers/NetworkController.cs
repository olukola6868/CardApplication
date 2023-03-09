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
    public class NetworkController : Controller
    {
        private readonly INetworkService _networkService;
        public NetworkController(INetworkService networkService)
        {
            _networkService = networkService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNetwork(CreateNetworkRequestModel model)
        {
            var response = await _networkService.Create(model);
            if (response.Status == true)
            {
                TempData["success"] = $"Created Successfully and your card pin is {response.Data.Name}";
                return RedirectToAction("ManagerBoard", "Manager");
            }
            else
            {
                TempData["error"] = "Wrong Input";
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _networkService.Delete(id);
            ViewBag.Message = "Customer deleted successfully";
            return RedirectToAction();
        }
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _networkService.Get(id);
            if (customer.Status == true)
            {
                return View(customer.Data);
            }
            return View();
        }
        public IActionResult GetAll()
        {
            var card = _networkService.GetAll();
            return View(card);
        }
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateNetworkRequestModel model)
        {
            var response = await _networkService.Update(id, model);
            if (response.Status == false)
            {
                TempData["Message"] = response.Message;
                return View();
            }
            return RedirectToAction("");
        }
    }
}