namespace GeneticAlgorithm;

public static class SelectionOperator
{
    public static Individual TournamentSelection(List<Individual> population, int tournamentSize, int loopCount = 1)
    {
        var random = new Random();  
        
        var chosenIndividuals = random.GetItems(population.ToArray(), tournamentSize);

        var individual = chosenIndividuals.ToList().GetBestIndividual();
        
        return individual;
    }

    public static Individual HotDeckSelection(List<Individual> population)
    {
        return population.GetBestIndividual();
    }

    private static Individual GetBestIndividual(this List<Individual> population)
    {
        return population.Aggregate((agg, next) => next.Fitness > agg.Fitness ? next : agg);    
    }
}