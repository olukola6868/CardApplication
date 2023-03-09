using System.Security.Claims;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HarnyCardApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequestModel model)
        {
            var create = await _customerService.Create(model);
            if (create.Status != false)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                TempData["error"] = "Wrong Input";
                return View("Create");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            ViewBag.Message = "Customer deleted successfully";
            return RedirectToAction();
        }
        public IActionResult FundWallet()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FundWallet(FundWalletRequestModel model)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = await _customerService.Get(int.Parse(id));
            var wallet = await _customerService.FundWallet(model, int.Parse(id));
            return RedirectToAction("CustomerBoard", "Customer");
        }
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = await _customerService.Get(int.Parse(userId));
            if (customer.Status == true)
            {
                return View(customer);
            }
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var customer = await _customerService.GetAll();
            if (customer.Status == true)
            {
                return View(customer.Data);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerRequestModel model)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var update = await _customerService.Update(int.Parse(id), model);
            if (update.Status != false)
            {
                return RedirectToAction("CustomerBoard", "Customer");
            }
            else
            {
                TempData["error"] = "Wrong Input";
                return View("Update");
            }
        }
        public IActionResult CustomerBoard()
        {
            return View();
        }
    }
}