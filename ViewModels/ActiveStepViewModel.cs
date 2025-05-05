using ReactiveUI;
using RestaurantSimulator.Models;

namespace RestaurantSimulator.ViewModels;
public class ActiveStepViewModel : ReactiveObject
{
    private int _currentClicks;
    private readonly RecipeStep _step;
    
    public ActiveStepViewModel(RecipeStep step)
    {
        _step = step;
        CurrentClicks = 0;
    }
    
    public string StepDescription => _step?.Step ?? "No step selected";
    public string StepImage => _step?.ImagePath ?? "assets/default_step.png";
    public int RequiredClicks => _step?.RequiredClicks ?? 0;
    
    public int CurrentClicks
    {
        get => _currentClicks;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentClicks, value);
            this.RaisePropertyChanged(nameof(ClickProgress));
        }
    }
    
    public double ClickProgress => RequiredClicks > 0 ? (double)CurrentClicks / RequiredClicks : 0;
    
    public void IncrementClick()
    {
        if (CurrentClicks < RequiredClicks)
            CurrentClicks++;
    }
}