using System.Security.Claims;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HarnyCardApplication.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        public IActionResult CreateCard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCard(CreateCardRequestModel model)
        {
            var response = await _cardService.Create(model);
            if (response.Status == true)
            {
                TempData["success"] = $"Created Successfully";
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
            _cardService.Delete(id);
            ViewBag.Message = "Card deleted successfully";
            return RedirectToAction();
        }
        public async Task<IActionResult> Get(int id)
        {
            var card = await _cardService.Get(id);
            if (card.Status == true)
            {
                return View(card.Data);
            }
            return View();
        }
        public async Task<IActionResult> BuyCard()
        {
            var card = await _cardService.GetIsAvailableCards();
            return View(card.Data);
        }
        [HttpPost]
        public async Task<IActionResult> BuyCard(BuyCardRequestModel buyCardRequest, int id)
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var card = await _cardService.BuyCard(buyCardRequest, int.Parse(Id), id);
            if (card == null)
            {
                ViewBag.error = "invalid";
                return View();
            }
            TempData["CardPin"] = card.Message;
            return RedirectToAction("BuyCard");
        }
        public async Task<IActionResult> GetAll()
        {
            var card = await _cardService.GetAll();
            return View(card);
        }
         public async Task<IActionResult> GetCustomerCards()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var card = await _cardService.GetCustomerCards(int.Parse(userId));
            return View(card);
        }
        public async Task<IActionResult> GetCardByPin(string pin)
        {
            var card = await _cardService.GetCardByPin(pin);
            if (card.Status == true)
            {
                return View(card.Data);
            }
            return View();
        }
    }
}