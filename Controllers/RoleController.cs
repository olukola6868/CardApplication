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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            var role = await _roleService.Create(model);
            if (role.Status == true)
            {
                return RedirectToAction("ManagerBoard", "User");
            }
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            if (roles.Status == true)
            {
                return View(roles.Data);
            }
            return View();
        }
        public IActionResult GetRoleByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}