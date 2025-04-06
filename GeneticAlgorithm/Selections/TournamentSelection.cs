namespace GeneticAlgorithm.Selections;

public class TournamentSelection(int tournamentSize, int roundCount, Func<List<Individual.Individual>, Individual.Individual> getBestIndividualFunc) : ISelection
{
    public List<Individual.Individual> SelectIndividuals(List<Individual.Individual> individuals)
    {
        var selectedIndividuals = new List<Individual.Individual>();
        
        var random = new Random();  
        for (var i = 0; i < roundCount; i++)
        {
            var tournamentRoundIndividuals = random.GetItems(individuals.ToArray(), tournamentSize).ToList();
            var winner = GetBestIndividual(tournamentRoundIndividuals);
            
            selectedIndividuals.Add(new Individual.Individual(winner));
        }
        
        return selectedIndividuals;
    }

    private Individual.Individual GetBestIndividual(List<Individual.Individual> individualsWithFitness)
    {
        return getBestIndividualFunc(individualsWithFitness);
    }
}