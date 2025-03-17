namespace GeneticAlgorithm;

public class Individual
{
    public Individual(Individual other)
    {
        Genotype = other.Genotype;
    }

    public Individual(byte[] genotype)
    {
        Genotype = genotype;
    }
    
    // TODO: Should be list of objects containing bytes?
    public byte[] Genotype {get; init;}

    public double Fitness { get; private set; }
}