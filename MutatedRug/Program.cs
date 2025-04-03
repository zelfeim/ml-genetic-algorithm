using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;

const int minParameterValue = 0;
const int maxParameterValue = 100;
const int chromosomeCount = 10;
const int iterations = 500;
const int individualCount = 9;
const int tournamentSize = 2;
const int parameterCount = 2;

var population = new Population(individualCount, iterations, chromosomeCount, parameterCount, minParameterValue, maxParameterValue);
var fitness = new FuncFitness(FitnessFunc);
var selection = new TournamentSelection(tournamentSize, parameterCount, SelectBest);
var mutationFunc = MutatePopulation;
var hotDeckSelection = SelectBest;

var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(population, fitness, selection, hotDeckSelection, mutationFunc);

geneticAlgorithm.Execute();

return 0;

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

