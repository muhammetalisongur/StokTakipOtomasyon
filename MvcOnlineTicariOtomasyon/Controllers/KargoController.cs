﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class KargoController : Controller
    {
        // GET: Kargo
        private Context c = new Context();
        public ActionResult Index(string p)
        {
            IQueryable<KargoDetay> k = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                k = k.Where(y => y.TakipKodu.Contains(p));
            }
            return View(k.ToList());
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random random = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "K" };
            int k1, k2, k3;
            k1 = random.Next(0, karakterler.Length);
            k2 = random.Next(0, karakterler.Length);
            k3 = random.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = random.Next(100, 1000); // 10=> 3 1 2 1 2 1
            s2 = random.Next(10, 99);
            s3 = random.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {
            c.KargoDetays.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}