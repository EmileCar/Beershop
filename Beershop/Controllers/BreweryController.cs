using AutoMapper;
using Beershop.Service.Interfaces;
using Beershop.Services;
using BeerShop.ViewModels;
using BeerStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beershop.Controllers
{
    public class BreweryController : Controller
    {
        private IService<Brewery> _breweryService;

        private readonly IMapper _mapper;

        public BreweryController(IMapper mapper, IService<Brewery> breweryService)
        {
            _mapper = mapper;
            _breweryService = breweryService;


        }

        public async Task<IActionResult> Index()  // add using System.Threading.Tasks;
        {

            var list = await _breweryService.GetAll();
            List<BreweryVM> listVM = _mapper.Map<List<BreweryVM>>(list);
            return View(listVM);


        }
    }
}
