using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using NetFlask.DAL.Repository;
using NetFlask.DAL.Repository.Entities;
using NetFlask.Web.Models;
using System.Diagnostics;

namespace NetFlask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IRepository<MoviesEntity, int> _Mrepo;
		private readonly IRepository<GenreEntity, int> _Grepo;

		public HomeController(ILogger<HomeController> logger, IRepository<MoviesEntity, int> Mrepo, 
            IRepository<GenreEntity, int> Grepo )
        {
            _logger = logger;
			_Mrepo = Mrepo;
			_Grepo = Grepo;
		}

        public IActionResult Index()
        {
			MoviesEntity me = _Mrepo.Get(1);
            List<GenreEntity> lgenre = (_Grepo as GenreRepository).GetByMovie(1).ToList();
            HomeModel hm = new HomeModel();
            HeaderMovie Frozen = new HeaderMovie()
            {
                Title = me.Title,
                Categorie = "Tout public",
                Description =me.Description,
                Directors =  "Chris Buck,Jennifer Lee", 
                Genre =string.Join(",",lgenre.Select(g=>g.Libelle)),
                PicturePath = me.PicturePath,
                Rating = me.Rating,
                ReleaseDate = me.ReleaseDate
            };
            hm.HeaderMovie = Frozen;

            return View(hm);
        }

        public IActionResult Privacy()
        {
			HomeModel hm = new HomeModel();
			HeaderMovie Frozen = new HeaderMovie()
			{
				Title = "La reine des neiges",
				Categorie = "Tout public",
				Description = @"Quand la nouvelle reine Elsa utilise accidentellement son pouvoir plongeant tout son royaume dans un hiver éternel, sa soeur Anna fait équipe avec un montagnard, son renne espiègle et un bonhomme de neige pour changer les conditions météorologiques.",
				Directors = "Chris Buck,Jennifer Lee",
				Genre = "Animation",
				PicturePath = "/images/frozen.jpg",
				Rating = 7.4,
				ReleaseDate = new DateTime(2013, 12, 4)
			};
			hm.HeaderMovie = Frozen;

			return View(hm);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}