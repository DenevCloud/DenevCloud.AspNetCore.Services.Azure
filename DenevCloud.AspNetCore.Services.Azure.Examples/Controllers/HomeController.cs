using DenevCloud.AspNetCore.Services.Azure.Examples.Models;
using DenevCloud.AspNetCore.Services.Azure.KeyVaults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.Examples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKeyVaultManager keyVaultManager;

        public HomeController(ILogger<HomeController> logger, IKeyVaultManager keyVaultManager)
        {
            _logger = logger;
            this.keyVaultManager = keyVaultManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("SaveSecret")]
        public IActionResult SaveSecret(string SecretName, string SecretValue)
        {
            keyVaultManager.SetNewSecret(SecretName, SecretValue);
            ViewBag.SecretSaved = true;
            return RedirectToAction("Index");
        }
    }
}
