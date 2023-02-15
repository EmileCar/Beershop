using Beershop.Extensions;
using Beershop.Services;
using Beershop.ViewModels;
using BeerStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Beershop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            return View(cartList);
        }

        public IActionResult Delete(int? bierNr)
        {
            if(bierNr == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            CartVM? itemToRemove = cartList?.Cart.FirstOrDefault(r => r.Biernr == bierNr);

            if(itemToRemove != null)
            {
                cartList?.Cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            //cartList.Cart.Remove();
            return View("Index", cartList);
        }
    }
}
