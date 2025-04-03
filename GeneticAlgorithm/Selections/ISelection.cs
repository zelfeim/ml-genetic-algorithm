namespace GeneticAlgorithm.Selections;

public interface ISelection
{
    public List<Individual.Individual> SelectIndividuals(List<Individual.Individual> individuals); 
}