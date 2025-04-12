using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;

namespace MutatedRug;

public class Solution
{
    private const int MinValue = 0;
    private const int MaxValue = 100;
    private const int GeneCount = 2;

    private readonly Population _population;
    private readonly IFitness _fitness;
    private readonly ISelection _selection;
    private readonly GeneticAlgorithm.GeneticAlgorithm _geneticAlgorithm;
    
    public Solution(int chromosomeCount = 3, int iterations = 500, int populationSize = 9, int tournamentSize = 2)
    {
        _population = new Population(populationSize, iterations, chromosomeCount, GeneCount, MinValue, MaxValue, SelectBest);
        _fitness = new FuncFitness(FitnessFunc);
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
        return individuals.MaxBy(individual => individual.Fitness)!;
    }

    private static double FitnessFunc(List<double> arguments)
    {
         var x1 = arguments[0];
         var x2 = arguments[1];
         
         return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
    }
}