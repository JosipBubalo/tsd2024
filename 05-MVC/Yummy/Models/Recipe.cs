﻿namespace Yummy.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Difficulty { get; set; }
        public int Likes { get; set; }
        public string Ingredients { get; set; }
        public string Process { get; set; }
        public string Tips { get; set; }
    }
}
