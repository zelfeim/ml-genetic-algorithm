namespace GeneticAlgorithm.Individual;

public class Individual
{
    public double Fitness;

    public Individual(Individual other)
    {
        Fitness = other.Fitness;
        Genotype = other.Genotype.ToList();
    }

    public Individual(IEnumerable<byte> genotype)
    {
        Genotype = genotype.ToList();
    }

    public List<byte> Genotype { get; init; }

    public void FlipBit(int index)
    {
        Genotype[index] ^= 1;
    }
}