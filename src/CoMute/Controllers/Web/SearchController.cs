﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models.DAL;

namespace CoMute.Web.Controllers.Web
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult CreateCarpool()
        {
            return View();
        }

        public ActionResult ViewCarpool()
        {
            return View();
        }
    }
}