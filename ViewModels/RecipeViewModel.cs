using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using RestaurantSimulator.Models;

namespace RestaurantSimulator.ViewModels;

public class RecipeViewModel : ReactiveObject
{
    private readonly Recipe? _recipe;
    
    public RecipeViewModel(Recipe? recipe)
    {
        _recipe = recipe;
    }
    
    public string Name => _recipe?.Name ?? string.Empty;
    public string Difficulty => _recipe?.Difficulty ?? string.Empty;
    public string RecipeImage => _recipe?.RecipeImagePath ?? "assets/default_recipe.png";
    public List<RecipeStep> Steps => _recipe?.Steps ?? new List<RecipeStep>();
    
    public double RecipeProgress
    {
        get
        {
            if (Steps == null || Steps.Count == 0)
                return 0;
                
            return (double)Steps.Count(s => s?.CurrentClicks >= s?.RequiredClicks) / Steps.Count;
        }
    }
}