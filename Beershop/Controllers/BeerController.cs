using AutoMapper;
using Beershop.Services;

using BeerShop.ViewModels;
using BeerStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BeerShop.Controllers
{
    public class BeerController : Controller
    {
        private BeerService beerService;
   
       private BreweryService breweryService;
        private VarietyService varietyService;


        private readonly IMapper _mapper;


        public BeerController(IMapper mapper)
        {
            _mapper = mapper;
            beerService = new BeerService();
            varietyService = new VarietyService();
            breweryService = new BreweryService();
      

        }
      

        public async Task<IActionResult> Index()  // add using System.Threading.Tasks;
        {

            var list = await beerService.GetAllAsync(); 
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
                Breweries = new SelectList(await breweryService.GetAllAsync()
                  , "Brouwernr", "Naam"),
                Varieties = new SelectList(await varietyService.GetAllAsync()
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
                    await beerService.AddAsync(beer);
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
                 new SelectList(await breweryService.GetAllAsync(), "Brouwernr", "Naam", entityVM.Brouwernr);

            entityVM.Varieties =
                new SelectList(await varietyService.GetAllAsync(), "Soortnr", "Soortnaam", entityVM.Soortnr);

            return View(entityVM);
        }

        // GET edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer? beer = await beerService.GetAsync(Convert.ToInt16(id));
            if (beer == null)
            {
                return NotFound();
            }

            var beerEdit = new BeerEditVM()
            {
                Biernr = beer.Biernr,
                Naam = beer.Naam,
                Alcohol = Convert.ToInt16(beer.Alcohol),
                Breweries = new SelectList(await breweryService.GetAllAsync()
                  , "Brouwernr", "Naam", beer.Brouwernr),
                Varieties = new SelectList(await varietyService.GetAllAsync()
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
                    await beerService.EditAsync(beer);
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
                 new SelectList(await breweryService.GetAllAsync(), "Brouwernr", "Naam", entityVM.Brouwernr);

            entityVM.Varieties =
                new SelectList(await varietyService.GetAllAsync(), "Soortnr", "Soortnaam", entityVM.Soortnr);

            return View(entityVM);
        }

        // GET edit
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer? beer = await beerService.GetAsync(Convert.ToInt16(id));
            if (beer == null)
            {
                return NotFound();
            }

            await beerService.DeleteAsync(beer);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Select(int? id)
        {
            return View();
        }


    }
}
