using AimyMovieStore.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace AimyMovieStore.Controllers
{
    public class CheckoutController : Controller
    {
        private MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies_Read([DataSourceRequest]DataSourceRequest request)
        {
            var cookie = Request.Cookies.Get("SelectedIds");
            if (cookie == null)
            {
                return Json(new { });
            }
            var ids = JsonConvert.DeserializeObject<int[]>(cookie.Value);
            var movies = ids.Select(id => db.Movies.Find(id));
            //IQueryable<Movie> movies = db.Movies;
            DataSourceResult result = movies.ToDataSourceResult(request, movie => new
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Price = movie.Price
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movies_Destroy([DataSourceRequest]DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var cookie = Request.Cookies.Get("SelectedIds");
                var ids = JsonConvert.DeserializeObject<List<int>>(cookie.Value);
                ids.Remove(movie.Id);
                cookie.Value = JsonConvert.SerializeObject(ids);
                Response.SetCookie(cookie);
                //                Request.Cookies.Set(cookie);
                //
                //                var entity = new Movie
                //                {
                //                    Id = movie.Id,
                //                    Name = movie.Name,
                //                    Genre = movie.Genre,
                //                    Price = movie.Price
                //                };
                //
                //                db.Movies.Attach(entity);
                //                db.Movies.Remove(entity);
                //                db.SaveChanges();
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
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
