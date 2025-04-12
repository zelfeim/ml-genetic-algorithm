using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using ValueComparison;

namespace ValueComparision;

public class Solution
{
    private const int MinValue = 0;
    private const int MaxValue = 3;
    private const int GeneCount = 3;
    
    private readonly Population _population;
    private readonly ComparisionFitness _fitness;
    private readonly TournamentSelection _selection;
    private readonly GeneticAlgorithm.GeneticAlgorithm _geneticAlgorithm;

    public Solution(int chromosomeCount = 4, int iterations = 500, int populationSize = 13, int tournamentSize = 3)
    {
        _population = new Population(populationSize, iterations, chromosomeCount, GeneCount, MinValue, MaxValue,
            SelectBest);
        _fitness = new ComparisionFitness(ReadCsvFile());
        _selection = new TournamentSelection(tournamentSize, populationSize, SelectBest);

        _geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(_population, _fitness, _selection, MutatePopulation,
            HelperDictionary);
    }

    public Dictionary<int, Tuple<double, double>> HelperDictionary { get; set; } = new();

    public void Run()
    {
        _geneticAlgorithm.Execute(); 
    }
    
    private static Individual SelectBest(List<Individual> individuals)
    {
        return individuals.MinBy(i => i.Fitness)!;
    }

    private static Dictionary<double, double> ReadCsvFile()
    {
        using var reader = new StreamReader(@"values.csv");

        var valueDictionary = new Dictionary<double, double>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line!.Split(',');

            valueDictionary.Add(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]));
        }

        return valueDictionary;
    }

    private List<Individual> MutatePopulation(List<Individual> individuals)
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
}