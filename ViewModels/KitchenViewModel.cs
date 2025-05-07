using System.Collections.Generic;
using ReactiveUI;
using RestaurantSimulator.Models;

namespace RestaurantSimulator.ViewModels;
public class KitchenViewModel : ReactiveObject
{
    private List<string> _activeEquipment = new List<string>();
    
    public void UpdateEquipment(List<string> equipmentNames)
    {
        ActiveEquipment = equipmentNames ?? new List<string>();
    }
    
    public List<string> ActiveEquipment
    {
        get => _activeEquipment;
        private set => this.RaiseAndSetIfChanged(ref _activeEquipment, value);
        Console.WriteLine(ActiveStepViewModel)
    }
}