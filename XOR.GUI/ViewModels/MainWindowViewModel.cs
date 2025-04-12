using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using XOR.GUI.Views;

namespace XOR.GUI.ViewModels;

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
            new Solution(ChromosomeCount, Iterations, IndividualCount, TournamentSize);
        solution.Run();

        var graphWindow = new GraphWindow()
        {
            DataContext = new GraphViewModel(solution.HelperDictionary),
        };
        graphWindow.Show();
    }
}
