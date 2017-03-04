using AimyMovieStore.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AimyMovieStore.Controllers
{
    public class MoviesController : Controller
    {
        private MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cart(string movieIds)
        {
            //var ids = JsonConvert.DeserializeObject<int[]>(data);
            //var movies = ids.Select(id => db.Movies.Find(id)).ToList();
            //save selected movie IDs to cookie
            var cookie = Request.Cookies.Get("SelectedIds");
            if (cookie == null)
            {
                Response.Cookies.Add(new HttpCookie("SelectedIds", movieIds));
            }
            else
            {
                cookie.Value = movieIds;
                Response.SetCookie(cookie);
            }
            return null;
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movies_Create([DataSourceRequest]DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var entity = new Movie
                {
                    Name = movie.Name,
                    Genre = movie.Genre,
                    Price = movie.Price
                };

                db.Movies.Add(entity);
                db.SaveChanges();
                movie.Id = entity.Id;
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movies_Update([DataSourceRequest]DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var entity = new Movie
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Genre = movie.Genre,
                    Price = movie.Price
                };

                db.Movies.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { movie }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movies_Destroy([DataSourceRequest]DataSourceRequest request, Movie movie)
        {
            if (ModelState.IsValid)
            {
                var entity = new Movie
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Genre = movie.Genre,
                    Price = movie.Price
                };

                db.Movies.Attach(entity);
                db.Movies.Remove(entity);
                db.SaveChanges();
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
