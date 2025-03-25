using System.Collections;

namespace GeneticAlgorithm;

public class Individual
{
    public Individual(Individual other)
    {
        Genotype = other.Genotype;
    }

    public Individual(IEnumerable<byte> genotype)
    {
        Genotype = genotype.ToList();
    }

    public List<byte> Genotype { get; init; }

    public double Fitness { get; set; }

    public void FlipBit(int index)
    {
        Genotype[index] ^= 1;
    }
}