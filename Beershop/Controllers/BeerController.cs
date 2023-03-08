using AutoMapper;
using Beershop.Extensions;
using Beershop.Service;
using Beershop.Service.Interfaces;
using Beershop.Services;
using Beershop.ViewModels;
using BeerShop.ViewModels;
using BeerStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BeerShop.Controllers
{
    public class BeerController : Controller
    {
        private IService<Beer> _beerService;
        private BreweryService _breweryService;
        private VarietyService _varietyService;

        private readonly IMapper _mapper;

        public BeerController(IMapper mapper, IService<Beer> beerService)
        {
            _mapper = mapper;
            //beerService = new BeerService();
            _beerService = beerService;
            _varietyService = new VarietyService();
            _breweryService = new BreweryService();
      

        }


        [Authorize]
        public async Task<IActionResult> Index()  // add using System.Threading.Tasks;
        {

            var list = await _beerService.GetAll(); 
            List<BeerVM> listVM = _mapper.Map<List<BeerVM>>(list);
            return View(listVM);


        }
        // Asynchronous code does introduce a small amount of overhead at run time, 
        // but for low traffic situations the performance hit is negligible, while for 
        // high traffic situations, the potential performance improvement is substantial.
        // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.0




        // GET: Beer/Create
        public async Task<IActionResult> Create()
        {

            var beerCreate = new BeerCreateVM()
            {
                Breweries = new SelectList(await _breweryService.GetAllAsync()
                  , "Brouwernr", "Naam"),
                Varieties = new SelectList(await _varietyService.GetAllAsync()
                  , "Soortnr", "Soortnaam")
            };

            return View(beerCreate);

        }


        //  POST: Beer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerCreateVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var beer = _mapper.Map<Beer>(entityVM);
                    await _beerService.Add(beer);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "call system administrator.");
            }

            entityVM.Breweries =
                 new SelectList(await _breweryService.GetAllAsync(), "Brouwernr", "Naam", entityVM.Brouwernr);

            entityVM.Varieties =
                new SelectList(await _varietyService.GetAllAsync(), "Soortnr", "Soortnaam", entityVM.Soortnr);

            return View(entityVM);
        }

        // GET edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer? beer = await _beerService.FindById(Convert.ToInt16(id));
            if (beer == null)
            {
                return NotFound();
            }

            var beerEdit = new BeerEditVM()
            {
                Biernr = beer.Biernr,
                Naam = beer.Naam,
                Alcohol = Convert.ToInt16(beer.Alcohol),
                Breweries = new SelectList(await _breweryService.GetAllAsync()
                  , "Brouwernr", "Naam", beer.Brouwernr),
                Varieties = new SelectList(await _varietyService.GetAllAsync()
                  , "Soortnr", "Soortnaam", beer.Soortnr)
            };

            return View(beerEdit);

        }

        //  POST: Beer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeerEditVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var beer = _mapper.Map<Beer>(entityVM);
                    await _beerService.Update(beer);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "call system administrator.");
            }

            entityVM.Breweries =
                 new SelectList(await _breweryService.GetAllAsync(), "Brouwernr", "Naam", entityVM.Brouwernr);

            entityVM.Varieties =
                new SelectList(await _varietyService.GetAllAsync(), "Soortnr", "Soortnaam", entityVM.Soortnr);

            return View(entityVM);
        }

        // GET edit
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer? beer = await _beerService.FindById(Convert.ToInt16(id));
            if (beer == null)
            {
                return NotFound();
            }

            await _beerService.Delete(beer);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Select(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Beer? beer = await _beerService.FindById(Convert.ToInt16(id));

            CartVM item = new CartVM
            {
                Biernr = beer.Biernr,
                Naam = beer.Naam,
                Aantal = 1,
                Prijs = 15,
                DateCreated = System.DateTime.Now
            };

            ShoppingCartVM shopping;

            // checken of al een session object is aangemaakt
            if(HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            } else {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            shopping?.Cart?.Add(item);

            HttpContext.Session.SetObject("ShoppingCart", shopping);

            // naar controller
            return RedirectToAction("Index", "ShoppingCart");
        }


    }
}
