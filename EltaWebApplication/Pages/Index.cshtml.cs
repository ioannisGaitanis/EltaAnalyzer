﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Model;

namespace EltaWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<CSearch> SearchesList { get; set; }

        public void OnGet()
        {
            Model.CWebApplicationContext context = HttpContext.RequestServices.GetService(typeof(Model.CWebApplicationContext)) as Model.CWebApplicationContext;
            this.SearchesList = context.GetAllAlbums();
            
        }
    }
}