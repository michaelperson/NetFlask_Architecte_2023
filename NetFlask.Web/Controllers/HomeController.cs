using Microsoft.AspNetCore.Mvc;
using NetFlask.Web.Models;
using System.Diagnostics;

namespace NetFlask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeModel hm = new HomeModel();
            HeaderMovie Frozen = new HeaderMovie()
            {
                Title = "La reine des neiges",
                Categorie = "Tout public",
                Description = @"Quand la nouvelle reine Elsa utilise accidentellement son pouvoir plongeant tout son royaume dans un hiver éternel, sa soeur Anna fait équipe avec un montagnard, son renne espiègle et un bonhomme de neige pour changer les conditions météorologiques.",
                Directors =  "Chris Buck,Jennifer Lee", 
                Genre = "Animation",
                PicturePath = "/images/frozen.jpg",
                Rating = 7.4,
                ReleaseDate = new DateTime(2013, 12, 4)
            };
            hm.HeaderMovie = Frozen;

            return View(hm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}