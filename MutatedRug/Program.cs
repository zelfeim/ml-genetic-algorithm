// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using GeneticAlgorithm;
using GeneticAlgorithm.Model;
using GeneticAlgorithm.Operators;

const int minParameterValue = 0;
const int maxParameterValue = 100;
const int chromosomeCount = 16;
const int iterations = 50;
const int individualCount = 17;
const int tournamentSize = 2;
const int parameterCount = 2;

var model = new Model();

var parameters = new List<Parameter>()
{
    new(minParameterValue, maxParameterValue, chromosomeCount),
    new(minParameterValue, maxParameterValue, chromosomeCount),
};

model.Parameters = parameters;

var individuals =
    IndividualFactory.CreateRandomIndividuals(chromosomeCount * parameterCount, individualCount);

CalculateIndividualsFitness(individuals);

for (var i = 0; i < iterations; i++)
{
    Console.WriteLine($"Iteration {i + 1}/{iterations}");
    var selectedIndividuals = SelectionOperator.TournamentSelection(individuals, tournamentSize, individuals.Count - 1);
    selectedIndividuals.ForEach(GeneticOperators.Mutate);

    selectedIndividuals.Add(SelectionOperator.HotDeckSelection(individuals));

    CalculateIndividualsFitness(selectedIndividuals);

    var bestFitness = selectedIndividuals.Select(i => i.Fitness).Max();
    var averageFitness = selectedIndividuals.Select(i => i.Fitness).Average();
    Console.WriteLine($"Best fitness: {bestFitness}");
    Console.WriteLine($"Average fitness: {averageFitness}\n");

    individuals = selectedIndividuals;
}

return 0;

double FitnessFunc(List<double> arguments)
{
     var x1 = arguments[0];
     var x2 = arguments[1];
     
     return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
}

void CalculateIndividualsFitness(List<Individual> individuals)
{
    foreach (var individual in individuals)
    {
        var arguments = model.DecodeGenotype(individual);
        individual.Fitness = FitnessFunc(arguments);
    }
}
