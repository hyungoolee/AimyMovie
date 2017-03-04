using AimyMovieStore.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AimyMovieStore.Controllers
{
    public class GridController : Controller
    {
        private MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Movie> movies = db.Movies;
            DataSourceResult result = movies.ToDataSourceResult(request, movie => new
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Price = movie.Price
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
