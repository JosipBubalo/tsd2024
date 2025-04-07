using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yummy.Models;

namespace Yummy.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            return View(RecipeStorage.GetAll());
        }

        public IActionResult Details(int id)
        {
            var recipe = RecipeStorage.Get(id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        public IActionResult Edit(int id)
        {
            var recipe = RecipeStorage.Get(id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            RecipeStorage.Update(recipe);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var recipe = RecipeStorage.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            RecipeStorage.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
