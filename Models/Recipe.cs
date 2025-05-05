namespace RestaurantSimulator.Models;

using System.Collections.Generic;

public class Recipe
{
    public string Name { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public List<string> Equipment { get; set; } = new List<string>();
    public string? RecipeImagePath { get; set; }
    public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
}