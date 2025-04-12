using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using MutatedRug.GUI.Views;

namespace MutatedRug.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{
    [ObservableProperty]
    [Required]
    private int _chromosomeCount = 3;

    [ObservableProperty]
    [Required]
    private int _individualCount = 9;
    
    [ObservableProperty]
    [Required]
    private int _iterations = 20;

    [ObservableProperty]
    [Required]
    private int _tournamentSize = 2;

    public void RunAlgorithm()
    {
        var solution =
            new Solution(ChromosomeCount, Iterations, IndividualCount, TournamentSize);
        
        solution.Run();

        var window = new GraphWindow()
        {
            DataContext = new GraphViewModel(solution.HelperDictionary)
        };
        window.Show();
    }
}