using miniproyecto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace miniproyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            IEnumerable<AlbumViewModel> albums = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                var response = client.GetAsync("albums");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsStringAsync();
                    read.Wait();

                    albums = JsonConvert.DeserializeObject<IList<AlbumViewModel>>(read.Result);
                }
                else
                {
                    albums = Enumerable.Empty<AlbumViewModel>();
                    ModelState.AddModelError(string.Empty, "Error al obtener datos del servidor");
                }

            }
            return View(albums);

        }

        public ActionResult Detalle(int idAlbum, string nombreAlbum="") {

            IEnumerable<FotoViewModel> photos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                var response = client.GetAsync("photos");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsStringAsync();
                    read.Wait();

                    photos = JsonConvert.DeserializeObject<IList<FotoViewModel>>(read.Result);

                    photos.Where(x => x.AlbumId == idAlbum);
                    ViewBag.NombreAlbum = nombreAlbum;
                }
                else
                {
                    photos = Enumerable.Empty<FotoViewModel>();
                    ModelState.AddModelError(string.Empty, "Error al obtener datos del servidor");
                }

            }


            return View(photos);
        }

        public ActionResult GetComentarios(int idPhoto) {

            IEnumerable<ComentarioViewModel> comments = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                var response = client.GetAsync("comments");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsStringAsync();
                    read.Wait();

                    comments = JsonConvert.DeserializeObject<IList<ComentarioViewModel>>(read.Result);
                    comments = comments.Where(x => x.PostId == idPhoto);
                }
                else
                {
                    comments = Enumerable.Empty<ComentarioViewModel>();
                    ModelState.AddModelError(string.Empty, "Error al obtener datos del servidor");
                }

            }

            return Json(JsonConvert.SerializeObject(comments), JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}