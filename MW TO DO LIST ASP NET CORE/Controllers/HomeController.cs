using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MW_TO_DO_LIST_ASP_NET_CORE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MW_TO_DO_LIST_ASP_NET_CORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Models.ToDo> _cases;

        public HomeController(ILogger<HomeController> logger, List<Models.ToDo> cases)
        {
            _logger = logger;
            _cases = cases;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_cases);
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            _cases.Add(new ToDo() { Name = name, Deleted = false });
            return RedirectToAction("Index");
            //return View(_cases);
        }

        [HttpGet]
        public IActionResult Delete(ToDo task)
        {
            //ToDo found = _cases.FirstOrDefault(x => x.Name == task.Name && x.Deleted == task.Deleted);
            int index = GetIndex(task);
            if (index != -1)
            {
                _cases[index].Deleted = true;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Check(ToDo task)
        {
            //ToDo found = _cases.FirstOrDefault(x => x.Name == task.Name && x.Deleted == task.Deleted);
            int index = GetIndex(task);
            if (index != -1)
            {
                _cases[index].Зачеркнуто = !_cases[index].Зачеркнуто;
            }
            return RedirectToAction("Index");
        }

        [NonAction]
        public int GetIndex(ToDo model)
        {
            return _cases.FindIndex(x => x.Name == model.Name && x.Deleted == model.Deleted);
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
    }
}
