namespace RestaurantSimulator.Models;

public class RecipeStep
{
    public string Step { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string? ImagePath { get; set; }
    public int RequiredClicks => Duration * 5;
    public int CurrentClicks { get; set; } = 0;
    public bool IsCompleted => CurrentClicks >= RequiredClicks;
}