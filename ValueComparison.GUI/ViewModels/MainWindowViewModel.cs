using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using LiveChartsCore.SkiaSharpView;
using ValueComparison;
using ValueComparison.GUI.Views;

namespace ValueComparison.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{
    [ObservableProperty]
    [Required]
    private int _chromosomeCount = 4;

    [ObservableProperty]
    [Required]
    private int _individualCount = 13;
    
    [ObservableProperty]
    [Required]
    private int _iterations = 100;

    [ObservableProperty]
    [Required]
    private int _tournamentSize = 3;

    public void RunAlgorithm()
    {
        var solution =
            new ValueComparision.Solution(ChromosomeCount, Iterations, IndividualCount, TournamentSize);
        solution.Run();
        
        var graphWindow = new GraphWindow()
        {
            DataContext = new GraphViewModel(solution.HelperDictionary)
        };
        graphWindow.Show();
    }
}