using GeneticAlgorithm.Individual;
using GeneticAlgorithm.Operators;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;
using ValueComparision;

const int minValue = 0;
const int maxValue = 3;
const int geneCount = 3;
const int chromosomeCount = 4;
const int iterations = 500;
const int populationSize = 13;
const int tournamentSize = 3;

var population = new Population(populationSize, iterations, chromosomeCount, geneCount, minValue, maxValue,
    SelectBestIndividual);
var fitness = new ComparisionFitness(ReadCsvFile());
var selection = new TournamentSelection(tournamentSize, populationSize, SelectBestIndividual);

var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(population, fitness, selection, MutatePopulation, new Dictionary<int, Tuple<double, double>>());

geneticAlgorithm.Execute();

return 0;

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