using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using XOR;

const int minParameterValue = -10;
const int maxParameterValue = 10;
const int chromosomeCount = 4;
const int iterations = 100;
const int individualCount = 13;
const int tournamentSize = 3;
const int parameterCount = 9;

var population = new Population(individualCount, iterations, chromosomeCount, parameterCount, minParameterValue, maxParameterValue, SelectBest);
var fitness = new NeuronFitness();
var selection = new TournamentSelection(tournamentSize, individualCount - 1, SelectBest);
var mutationFunc = MutatePopulation;

var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(population, fitness, selection, mutationFunc, new Dictionary<int, Tuple<double, double>>());

geneticAlgorithm.Execute();

return 0;

List<Individual> MutatePopulation(List<Individual> individuals)
{
    return individuals.Select(Mutation.FlipBit).ToList();
}

Individual SelectBest(List<Individual> individuals)
{
    return individuals.MinBy(individual => individual.Fitness);
}
