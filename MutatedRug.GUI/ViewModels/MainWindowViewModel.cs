using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using MutatedRug.Views;
using Tmds.DBus.Protocol;

namespace MutatedRug.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{
    private const int MinParameterValue = 0;
    private const int MaxParameterValue = 100;
    private const int ParameterCount = 2;

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
        var population = new Population(IndividualCount, Iterations, ChromosomeCount, ParameterCount, MinParameterValue, MaxParameterValue, SelectBest);
        var fitness = new FuncFitness(FitnessFunc);
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
        return individuals.MaxBy(individual => individual.Fitness);
    }

    double FitnessFunc(List<double> arguments)
    {
        var x1 = arguments[0];
        var x2 = arguments[1];

        return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
    }

    public static Dictionary<int, Tuple<double, double>> _helperDictionary = new();
}