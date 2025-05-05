using System.Collections.Generic;
using ReactiveUI;
using System.Linq;
using RestaurantSimulator.Models;

namespace RestaurantSimulator.ViewModels;




public class InventoryViewModel : ReactiveObject
{
    public InventoryViewModel(List<Ingredient> ingredients, Dictionary<string, string> icons)
    {
        Items = new List<InventoryItem>();
        
        if (ingredients != null && icons != null)
        {
            Items = ingredients.Select(i => new InventoryItem
            {
                Name = i.Name ?? string.Empty,
                Quantity = i.Quantity ?? string.Empty,
                Unit = i.Unit ?? string.Empty,
                IconPath = icons.TryGetValue(i.Name ?? string.Empty, out var icon) 
                    ? icon 
                    : "assets/default_icon.png"
            }).ToList();
        }
    }
    
    public List<InventoryItem> Items { get; } = new List<InventoryItem>();
}

public class InventoryItem
{
    public string Name { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string IconPath { get; set; } = "assets/default_icon.png";
}