using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CASA_CAMBIO.Models;
using System.Text.Json;

namespace CASA_CAMBIO.Controllers
{
    
    public class CambioController : Controller
    {
        private readonly ILogger<CambioController> _logger;

        public CambioController(ILogger<CambioController> logger)
        {
            _logger = logger;
        }

        private readonly Dictionary<string, decimal> _tasasCambio = new()
        {
            {"USD_PEN", 3.73m},
            {"EUR_PEN", 4.02m},
            {"PEN_USD", 0.27m},
            {"PEN_EUR", 0.25m},
            {"USD_EUR", 0.93m},
            {"EUR_USD", 1.07m}
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calcular(CambioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            string claveCambio = $"{model.MonedaOrigen}_{model.MonedaDestino}";
            
            if (_tasasCambio.ContainsKey(claveCambio))
            {
                model.TipoCambio = _tasasCambio[claveCambio];
                model.Resultado = model.Cantidad * model.TipoCambio;
            }
            else if (model.MonedaOrigen == model.MonedaDestino)
            {
                model.TipoCambio = 1m;
                model.Resultado = model.Cantidad;
            }
            else
            {
                ModelState.AddModelError("", "Tipo de cambio no disponible");
                return View("Index", model);
            }

            TempData["Transaccion"] = System.Text.Json.JsonSerializer.Serialize(model);
            return RedirectToAction("Boleta");
        }   

[HttpGet]
public IActionResult Boleta()
{
    if (TempData["Transaccion"] == null)
    {
        return RedirectToAction("Index");
    }

    var transaccion = System.Text.Json.JsonSerializer.Deserialize<CambioModel>(
        TempData["Transaccion"].ToString());

    var model = new ClienteModel { Transaccion = transaccion };
    return View(model);
}

[HttpPost]
public IActionResult Boleta(ClienteModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }
    return View("BoletaGenerada", model);
}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    
    
    }
}