namespace GeneticAlgorithm.Operators;

public static class SelectionOperator
{
    public static List<Individual> TournamentSelection(List<Individual> individuals, int tournamentSize, int iterations = 1)
    {
        var selectedIndividuals = new List<Individual>();
        
        var random = new Random();  
        for (var i = 0; i < iterations; i++)
        {
            var chosenIndividuals = random.GetItems(individuals.ToArray(), tournamentSize);
            var winner = chosenIndividuals.GetBestIndividual();
            
            selectedIndividuals.Add(new Individual(winner));
        }
        
        return selectedIndividuals.ToList();
    }

    public static Individual HotDeckSelection(List<Individual> population)
    {
        return population.ToArray().GetBestIndividual();
    }

    private static Individual GetBestIndividual(this Individual[] population)
    {
        return population.Aggregate((agg, next) => next.Fitness > agg.Fitness ? next : agg);    
    }
}