using System.Collections;

namespace GeneticAlgorithm.Operators;

public static class GeneticOperators
{
    // Can also implement with random values (for example exchange 1, 2, 6 byte) other than cutoff
    public static List<Individual> Crossover(Individual firstParent, Individual secondParent, int cutoff = 0)
    {
        if (firstParent.Genotype.Count != secondParent.Genotype.Count)
            throw new Exception("Genotype sizes are not the same!");

        var genotypeSize = firstParent.Genotype.Count;

        var firstParentBits = firstParent.Genotype.Cast<bool>().ToArray();
        var secondParentBits = secondParent.Genotype.Cast<bool>().ToArray();

        var random = new Random();
        var byteCutoff = cutoff != 0 ? cutoff : random.Next(genotypeSize);

        var firstChildBits = firstParentBits[.. byteCutoff].Concat(secondParentBits[byteCutoff..genotypeSize]).ToArray();
        var secondChildBits = secondParentBits[.. byteCutoff].Concat(firstParentBits[byteCutoff..genotypeSize]).ToArray();

        var children = new List<Individual>
        {
            new(firstChildBits),
            new(secondChildBits)
        };

        return children;
    }
    
    public static void Mutate(Individual individual)
    {
        var random = new Random();

        var bitIndex = random.Next(individual.Genotype.Count - 1);
        individual.FlipBit(bitIndex);
    }
}