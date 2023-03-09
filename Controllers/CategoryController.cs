using HarnyCardApplication.Dtos;
using HarnyCardApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HarnyCardApplication.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestModel model)
        {
            var createCategory = await _categoryService.Create(model);
            if (createCategory.Status == true)
            {
                TempData["Message"] = createCategory.Message;
                return RedirectToAction("ManagerBoard", "Manager");
            }
            return View();

        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.Delete(id);
            if (response.Status == false)
            {
                TempData["Message"] = response.Message;
                return View();
            }
            return RedirectToAction("");
        }
        public IActionResult Update()
        {
            return View();
        }
        public async Task<IActionResult> Update(int id, UpdateCategoryRequestModel model)
        {
            var response = await _categoryService.Update(id, model);
            if (response.Status == false)
            {
                TempData["Message"] = response.Message;
                return View();
            }
            return RedirectToAction("");
        }
    }
}