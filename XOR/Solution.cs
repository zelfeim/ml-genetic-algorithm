using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;

namespace XOR;
public class Solution
{
    private const int MinValue = -10;
    private const int MaxValue = 10;
    private const int GeneCount = 9;

    private readonly Population _population;
    private readonly IFitness _fitness;
    private readonly ISelection _selection;
    private readonly GeneticAlgorithm.GeneticAlgorithm _geneticAlgorithm;
    
    public Solution(int chromosomeCount = 4, int iterations = 100, int populationSize = 13, int tournamentSize = 3)
    {
        _population = new Population(populationSize, iterations, chromosomeCount, GeneCount, MinValue, MaxValue, SelectBest);
        _fitness = new NeuronFitness();
        _selection = new TournamentSelection(tournamentSize, populationSize - 1, SelectBest);
        
        _geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(_population, _fitness, _selection, MutatePopulation, HelperDictionary);
    }

    public void Run()
    {
        _geneticAlgorithm.Execute();
    }

    public Dictionary<int, Tuple<double, double>> HelperDictionary { get; set; } = new();

    private static List<Individual> MutatePopulation(List<Individual> individuals)
    {
        return individuals.Select(Mutation.FlipBit).ToList();
    }

    private static Individual SelectBest(List<Individual> individuals)
    {
        return individuals.MinBy(individual => individual.Fitness)!;
    }
}
