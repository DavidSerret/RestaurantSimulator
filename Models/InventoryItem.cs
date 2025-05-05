namespace RestaurantSimulator.Models;

public class InventoryItem
{
    public string Name { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string IconPath { get; set; } = "assets/default_icon.png";
}