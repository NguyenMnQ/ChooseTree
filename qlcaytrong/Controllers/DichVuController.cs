﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlcaytrong.Controllers
{
    public class DichVuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
