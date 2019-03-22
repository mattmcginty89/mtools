using MTools.Models;
using MTools.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace MTools.Controllers
{
    public class AlphabetizeController : Controller
    {
        private readonly IAlphabetizeService _alphabetizeService;

        public AlphabetizeController(IAlphabetizeService alphabetizeService)
        {
            _alphabetizeService = alphabetizeService;
        }

        [HttpGet]
        public IActionResult Alphabetize()
        {
            return View(new AlphabetizeModel());
        }

        [HttpPost]
        public IActionResult Alphabetize(AlphabetizeModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.InputText))
            {
                return View(model);
            }

            string[] lines = model.InputText.Split('\n').Where(line => !String.IsNullOrWhiteSpace(line)).ToArray();

            _alphabetizeService.AlphabetizeArray(ref lines);

            model.OutputText = String.Join("\n", lines);

            return View(model);
        }      
    }
}
