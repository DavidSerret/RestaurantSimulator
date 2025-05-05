using ReactiveUI;
using RestaurantSimulator.Models;
using System;
using System.Reactive;

namespace RestaurantSimulator.ViewModels;

public class ActiveStepViewModel : ReactiveObject
{
    private int _currentClicks;
    private readonly RecipeStep _step;
    
    public ActiveStepViewModel(RecipeStep step)
    {
        _step = step ?? throw new ArgumentNullException(nameof(step));
        CurrentClicks = 0;
        
        // Inicializa el comando
        IncrementClickCommand = ReactiveCommand.Create(IncrementClick);
    }
    
    // Comando para binding con la vista
    public ReactiveCommand<Unit, Unit> IncrementClickCommand { get; }
    
    public string StepDescription => _step.Step;
    public string StepImage => _step.ImagePath ?? "assets/default_step.png";
    public int RequiredClicks => _step.RequiredClicks;
    
    public int CurrentClicks
    {
        get => _currentClicks;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentClicks, value);
            this.RaisePropertyChanged(nameof(ClickProgress));
            this.RaisePropertyChanged(nameof(IsCompleted));
        }
    }
    
    public double ClickProgress => RequiredClicks > 0 ? (double)CurrentClicks / RequiredClicks : 0;
    
    // Nueva propiedad para binding con RecipeView
    public bool IsCompleted => CurrentClicks >= RequiredClicks;
    
    private void IncrementClick()
    {
        if (CurrentClicks < RequiredClicks)
        {
            CurrentClicks++;
        }
    }
}