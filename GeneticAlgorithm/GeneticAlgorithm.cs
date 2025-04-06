using GeneticAlgorithm.Fitness;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Selections;

namespace GeneticAlgorithm;

public class GeneticAlgorithm(
    Population population,
    IFitness fitness,
    ISelection selection,
    Func<List<Individual.Individual>, List<Individual.Individual>> mutatePopulationFunc
)
{
    public void Execute()
    {
        population.CreateInitialGeneration();
        CalculateFitness(population.Individuals);
        
        Run();
    }

    private void Run()
    {
        while (population.GenerationsCount > 0)
        {
            var newGeneration = selection.SelectIndividuals(population.Individuals);
            
            newGeneration = mutatePopulationFunc(newGeneration);

            var bestOldIndividual = new Individual.Individual(population.GetBestIndividual());
            newGeneration.Add(bestOldIndividual);
            
            CalculateFitness(newGeneration); 
            
            population.CreateNewGeneration(newGeneration);
            WriteBestAndAverageFitness();
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

    private void WriteBestAndAverageFitness()
    {
        var best = population.GetBestIndividual().Fitness;
        var average = population.Individuals.Average(individual => individual.Fitness);

        Console.WriteLine($"Best fitness: {best}, Average fitness: {average}");
    }
}