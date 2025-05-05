using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace RestaurantSimulator.Models;
public class RecipeData
{
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    public Dictionary<string, string> IngredientIcons { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, string> EquipmentImages { get; set; } = new Dictionary<string, string>();

    public static RecipeData LoadFromJsons(string recipesJson, string imagesJson)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Cargar recipes.json original
        var recipesData = JsonSerializer.Deserialize<RecipeData>(recipesJson, options) ?? new RecipeData();

        // Cargar recipe_images.json
        var imagesData = JsonSerializer.Deserialize<ImagesData>(imagesJson, options) ?? new ImagesData();

        // Combinar datos de imágenes con recetas e ingredientes
        foreach (var recipe in recipesData.Recipes)
        {
            var recipeImageInfo = imagesData.RecipeImages?.FirstOrDefault(r => r.Name == recipe.Name);
            if (recipeImageInfo != null)
            {
                recipe.RecipeImagePath = recipeImageInfo.Image;
                foreach (var step in recipe.Steps)
                {
                    var stepImage = recipeImageInfo.StepImages?.FirstOrDefault(s => s.Step == step.Step);
                    if (stepImage != null) step.ImagePath = stepImage.Image;
                }
            }
        }

        // Mapear iconos de ingredientes y equipamiento
        recipesData.IngredientIcons = imagesData.IngredientIcons ?? new Dictionary<string, string>();
        recipesData.EquipmentImages = imagesData.EquipmentImages ?? new Dictionary<string, string>();

        return recipesData;
    }
}

// Clase auxiliar para deserializar recipe_images.json
public class ImagesData
{
    public List<RecipeImageInfo>? RecipeImages { get; set; }
    public Dictionary<string, string>? EquipmentImages { get; set; }
    public Dictionary<string, string>? IngredientIcons { get; set; }
}

public class RecipeImageInfo
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<StepImage>? StepImages { get; set; }
}

public class StepImage
{
    public string Step { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}