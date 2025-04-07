using System.Collections.Generic;
using System.Linq;

namespace Yummy.Models
{
    public static class RecipeStorage
    {
        private static List<Recipe> _recipes = new List<Recipe>
        {
            new Recipe {
                ID = 1,
                Name = "Spaghetti Bolognese",
                Time = "30 mins",
                Difficulty = "Medium",
                Likes = 5,
                Ingredients = "Spaghetti, minced meat, tomatoes",
                Process = "Cook pasta, prepare sauce, mix",
                Tips = "Use fresh basil"
            },
            new Recipe {
                ID = 2,
                Name = "Pancakes",
                Time = "15 mins",
                Difficulty = "Easy",
                Likes = 10,
                Ingredients = "Flour, eggs, milk, sugar",
                Process = "Mix ingredients, fry on pan",
                Tips = "Add vanilla for extra flavor"
            }
        };

        public static List<Recipe> GetAll() => _recipes;

        public static Recipe Get(int id) => _recipes.FirstOrDefault(r => r.ID == id);

        public static void Update(Recipe updated)
        {
            var existing = Get(updated.ID);
            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Time = updated.Time;
                existing.Difficulty = updated.Difficulty;
                existing.Likes = updated.Likes;
                existing.Ingredients = updated.Ingredients;
                existing.Process = updated.Process;
                existing.Tips = updated.Tips;
            }
        }

        public static void Delete(int id)
        {
            var recipe = Get(id);
            if (recipe != null) _recipes.Remove(recipe);
        }
    }
}
