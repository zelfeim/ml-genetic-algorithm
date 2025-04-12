using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using ValueComparision;
using ValueComparison.GUI.Views;

namespace ValueComparison.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableValidator
{
    private const int MinParameterValue = 0;
    private const int MaxParameterValue = 3;
    private const int ParameterCount = 3;

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
        var population = new Population(IndividualCount, Iterations, ChromosomeCount, ParameterCount, MinParameterValue, MaxParameterValue, SelectBestIndividual);
        var fitness = new ComparisionFitness(ReadCsvFile());
        var selection = new TournamentSelection(TournamentSize, IndividualCount - 1, SelectBestIndividual);
        var mutationFunc = MutatePopulation;

        var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(population, fitness, selection, mutationFunc, _helperDictionary);

        geneticAlgorithm.Execute();

        var window = new GraphWindow();
        window.Show();
    }
        
    Individual SelectBestIndividual(List<Individual> individuals)
    {
        return individuals.MinBy(i => i.Fitness);
    }

    Dictionary<double, double> ReadCsvFile()
    {
        using var reader = new StreamReader(@"values.csv");

        var valueDictionary = new Dictionary<double, double>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');

            valueDictionary.Add(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]));
        }

        return valueDictionary;
    }

    List<Individual> MutatePopulation(List<Individual> individuals)
    {
        // Crossover population 
        var mutatedIndividuals = new List<Individual>();

        // Uwielbiam wymagania z czapy :)
        mutatedIndividuals.AddRange(Mutation.Crossover(individuals[0], individuals[1]));
        mutatedIndividuals.AddRange(Mutation.Crossover(individuals[2], individuals[3]));
        mutatedIndividuals.AddRange(individuals[4..8]);
        mutatedIndividuals.AddRange(Mutation.Crossover(individuals[8], individuals[9]));
        mutatedIndividuals.AddRange(individuals[10..^2]);
        mutatedIndividuals.AddRange(Mutation.Crossover(individuals[^2], individuals[^1]));

        // Mutate selected
        var newGeneration = mutatedIndividuals.Take(5).ToList();
        newGeneration.AddRange(mutatedIndividuals.Skip(5).Select(Mutation.FlipBit));

        return newGeneration.ToList();
    }

    public static Dictionary<int, Tuple<double, double>> _helperDictionary = new();
}