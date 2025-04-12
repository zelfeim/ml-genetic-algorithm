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
    private const int MinParameterValue = -10;
    private const int MaxParameterValue = 10;
    private const int ParameterCount = 9;

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
        var population = new Population(IndividualCount, Iterations, ChromosomeCount, ParameterCount, MinParameterValue, MaxParameterValue, SelectBest);
        var fitness = new NeuronFitness();
        var selection = new TournamentSelection(TournamentSize, IndividualCount - 1, SelectBest);
        var mutationFunc = MutatePopulation;

        var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(population, fitness, selection, mutationFunc, _helperDictionary);

        geneticAlgorithm.Execute();

        var window = new GraphWindow();
        window.Show();
    }

    List<Individual> MutatePopulation(List<Individual> individuals)
    {
        return individuals.Select(Mutation.FlipBit).ToList();
    }

    Individual SelectBest(List<Individual> individuals)
    {
        return individuals.MinBy(individual => individual.Fitness);
    }
    
    public static Dictionary<int, Tuple<double, double>> _helperDictionary = new();
}
