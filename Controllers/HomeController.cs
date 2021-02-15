using miniproyecto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using miniproyecto.Auxiliar;

namespace miniproyecto.Controllers
{
    public class HomeController : Controller
    {

        private readonly string _urlPrincipal;
        private readonly string _apiALBUMS;
        private readonly string _apiFOTOS;
        private readonly string _apiCOMMENTS;

        public HomeController()
        {

            _urlPrincipal = ConfigurationManager.AppSettings.Get("urlPrincipal"); //"https://jsonplaceholder.typicode.com";
            _apiALBUMS = ConfigurationManager.AppSettings.Get("apiAlbums");//"albums";
            _apiFOTOS = ConfigurationManager.AppSettings.Get("apiFotos");//"photos";
            _apiCOMMENTS = ConfigurationManager.AppSettings.Get("apiComments");//"comments";
        }


        public ActionResult Index()
        {
            IEnumerable<AlbumViewModel> albums = null;
            var GetHttp = new GetDataHttp<AlbumViewModel>(_urlPrincipal, _apiALBUMS);

            string error = string.Empty;
            albums = GetHttp.GetT(ref error);

            if (string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(albums);

        }

        public ActionResult Detalle(int? idAlbum, string nombreAlbum = "")
        {

            IEnumerable<FotoViewModel> photos = null;


            if (idAlbum != null)
            {
                var GetHttp = new GetDataHttp<FotoViewModel>(_urlPrincipal, _apiFOTOS);

                string error = string.Empty;
                photos = GetHttp.GetT(ref error);

                photos.Where(x => x.AlbumId == idAlbum);
                ViewBag.NombreAlbum = nombreAlbum;

                if (string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(photos);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Parámetros no válidos");
            }

            return View(photos);
        }

        public ActionResult GetComentarios(int idPhoto)
        {

            IEnumerable<ComentarioViewModel> comments = null;

            var GetHttp = new GetDataHttp<ComentarioViewModel>(_urlPrincipal, _apiCOMMENTS);

            string error = string.Empty;
            comments = GetHttp.GetT(ref error);

            comments = comments.Where(x => x.PostId == idPhoto);

            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return Json(JsonConvert.SerializeObject(comments), JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            var model = new AcercaDeViewModel()
            {
                Nombre = "Andy",
                Apellidos = "Del Aguila Rios"
            };

            return View(model);
        }

    }
}