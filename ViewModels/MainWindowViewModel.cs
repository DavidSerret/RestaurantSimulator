using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.IO;
using System;
using RestaurantSimulator.Models;

namespace RestaurantSimulator.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private Recipe? _selectedRecipe;
    private int _currentStepIndex;
    private ActiveStepViewModel _activeStep;
    
    public MainWindowViewModel()
    {
        // Initialize with empty/default values
        _activeStep = new ActiveStepViewModel(new RecipeStep
        {
            Step = string.Empty,
            Duration = 0,
            ImagePath = "assets/default_step.png"
        });
        
        // Load data from JSON
        try
        {
            var recipesJson = File.ReadAllText("recipes.json");
            var imagesJson = File.ReadAllText("recipe_images.json");
            var recipeData = RecipeData.LoadFromJsons(recipesJson, imagesJson);
            
            Recipes = recipeData.Recipes ?? new List<Recipe>();
            Inventory = new InventoryViewModel(
                recipeData.Ingredients ?? new List<Ingredient>(), 
                recipeData.IngredientIcons ?? new Dictionary<string, string>()
            );
            
            // Initialize with first recipe if available
            SelectedRecipe = Recipes.FirstOrDefault() ?? new Recipe
            {
                Name = "No Recipe Available",
                Difficulty = "N/A",
                Equipment = new List<string>(),
                Steps = new List<RecipeStep>()
            };
        }
        catch (Exception ex)
        {
            // Handle loading errors
            Console.WriteLine($"Error loading data: {ex.Message}");
            Recipes = new List<Recipe>();
            Inventory = new InventoryViewModel(new List<Ingredient>(), new Dictionary<string, string>());
        }
    }
    
    public List<Recipe> Recipes { get; }
    public InventoryViewModel Inventory { get; }
    public KitchenViewModel Kitchen { get; } = new KitchenViewModel();
    
    public Recipe? SelectedRecipe
    {
        get => _selectedRecipe;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
            CurrentStepIndex = 0;
            Kitchen.UpdateEquipment(value?.Equipment ?? new List<string>());
        }
    }
    
    public int CurrentStepIndex
    {
        get => _currentStepIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentStepIndex, value);
            var currentStep = SelectedRecipe?.Steps?.ElementAtOrDefault(value) ?? new RecipeStep
            {
                Step = string.Empty,
                Duration = 0,
                ImagePath = "assets/default_step.png"
            };
            ActiveStep = new ActiveStepViewModel(currentStep);
        }
    }
    
    public ActiveStepViewModel ActiveStep
    {
        get => _activeStep;
        private set => this.RaiseAndSetIfChanged(ref _activeStep, value);
    }
}