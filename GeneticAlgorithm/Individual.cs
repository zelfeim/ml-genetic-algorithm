using System.Collections;

namespace GeneticAlgorithm;

public class Individual
{
    public Individual(Individual other)
    {
        Genotype = other.Genotype;
    }

    public Individual(BitArray genotype)
    {
        Genotype = genotype;
    }

    public Individual(bool[] genotype)
    {
        Genotype = new BitArray(genotype);
    }

    public BitArray Genotype { get; init; }

    public double Fitness { get; set; }

    public void FlipBit(int index)
    {
        Genotype[index] ^= true;
    }
}