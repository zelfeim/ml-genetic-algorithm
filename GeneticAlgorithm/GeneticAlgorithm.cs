using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;

namespace GeneticAlgorithm;

public class GeneticAlgorithm(
    Population population,
    IFitness fitness,
    ISelection selection,
    Func<List<Individual.Individual>, Individual.Individual> selectBestFunc,
    Func<List<Individual.Individual>, List<Individual.Individual>> mutatePopulationFunc
)
{
    public void Execute()
    {
        population.CreateInitialGeneration();
        
        Run();
    }

    private void Run()
    {
        while (population.GenerationsCount > 0)
        {
            CalculateFitness(population.Individuals);

            // Create the copy of individuals
            var newGeneration = selection.SelectIndividuals(population.Individuals).ToList();
            
            newGeneration = mutatePopulationFunc(newGeneration);

            var bestOldIndividual = new Individual.Individual(selectBestFunc(population.Individuals));
            newGeneration.Add(bestOldIndividual);
            
            CalculateFitness(population.Individuals); 
            
            WriteBestAndAverageFitness(newGeneration);
            
            population.CreateNewGeneration(newGeneration);
            population.GenerationsCount--;
        }
    }

    private void CalculateFitness(List<Individual.Individual> individuals)
    {
        foreach (var individual in individuals)
        {
            var funcArguments = DecodeGenotype(individual);
            individual.Fitness = fitness.Calculate(funcArguments);
        }
    }

    private List<double> DecodeGenotype(Individual.Individual individual)
    {
        var values = new List<double>();

        var bits = individual.Genotype;

        for (var i = 0; i < population.GeneCount; i++)
        {
            var parameterBits = bits.Skip(i * population.GeneCount).Take(population.ChromosomesCount);
            
            values.Add(population.GenotypeLookupTable.FirstOrDefault(kvp => kvp.Key.SequenceEqual(parameterBits)).Value);
        }

        return values;
    }

    private void WriteBestAndAverageFitness(List<Individual.Individual> individuals)
    {
        var best = selectBestFunc(individuals).Fitness;
        var average = individuals.Average(individual => individual.Fitness);

        Console.WriteLine($"Best fitness: {best}, Average fitness: {average}");
    }
}