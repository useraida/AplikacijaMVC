using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelApp.Models;
using HotelApp.Security;

namespace HotelApp.Controllers
{
    public class SlikeHomeController : Controller
    {
        private HotelEntities db = new HotelEntities();
        // GET: SlikeHome
        public ActionResult Index(string filter = null, int stranica = 1, int velicinaStranice = 20)
        {
            var lista = new PretrazivanjeSlika<Slika>();
            ViewBag.filter = filter;

            lista.Sadrzaj = db.Slika
                            .Where(x => filter == null || (x.Opis.Contains(filter)))
                            .OrderByDescending(x => x.SlikaID)
                            .Skip((stranica - 1) * velicinaStranice)
                            .Take(velicinaStranice)
                            .ToList();

            // Brojanje
            lista.Ukupno = db.Slika
                           .Where(x => filter == null || (x.Opis.Contains(filter))).Count();
            lista.TrenutnaStranica = stranica;
            lista.VelicinaStranice = velicinaStranice;
            return View(lista);
        }

        public Size NovaVelicinaSlike(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }

        private void SnimiUFolder(Image slika, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NovaVelicinaSlike(slika.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(slika, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), slika.RawFormat);
            }
        }

        
        [HttpGet]
        [AuthorizeRoles("Admin")]
        public ActionResult Create()
        {
            var slika = new Slika();
            return View(slika);
        }

        [HttpPost]
        [AuthorizeRoles("Admin")]
        public ActionResult Create(Slika photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(photo);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Molimo izaberite sliku";
                return View(photo);
            }

            var model = new Slika();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Opis = photo.Opis;
                var fileName = Guid.NewGuid().ToString();
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                using (var img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    model.ThumbStaza = String.Format("/GalleryImages/thumbs/{0}{1}", fileName, extension);
                    model.SlikaStaza = String.Format("/GalleryImages/{0}{1}", fileName, extension);

                    // Save thumbnail size image, 100 x 100
                    SnimiUFolder(img, fileName, extension, new Size(100, 100), model.ThumbStaza);

                    // Save large size image, 800 x 800
                    SnimiUFolder(img, fileName, extension, new Size(600, 600), model.SlikaStaza);
                }

                // Save record to database
                model.DatumKreiranja = DateTime.Now;
                db.Slika.Add(model);
                db.SaveChanges();
            }

            return RedirectPermanent("/SlikeHome");
        }
    }
}