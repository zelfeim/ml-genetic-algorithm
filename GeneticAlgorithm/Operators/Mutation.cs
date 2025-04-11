namespace GeneticAlgorithm.Operators;

public static class Mutation
{
    public static List<Individual.Individual> Crossover(Individual.Individual firstParent,
        Individual.Individual secondParent, int cutoff = 0)
    {
        if (firstParent.Genotype.Count != secondParent.Genotype.Count)
            throw new Exception("Genotype sizes are not the same!");

        var genotypeSize = firstParent.Genotype.Count;

        var firstParentBits = firstParent.Genotype;
        var secondParentBits = secondParent.Genotype;

        var random = new Random();
        var byteCutoff = cutoff != 0 ? cutoff : random.Next(genotypeSize);

        var firstChildBits = firstParentBits[.. byteCutoff].Concat(secondParentBits[byteCutoff..genotypeSize]);
        var secondChildBits = secondParentBits[.. byteCutoff].Concat(firstParentBits[byteCutoff..genotypeSize]);

        var children = new List<Individual.Individual>
        {
            new(firstChildBits),
            new(secondChildBits)
        };

        return children;
    }

    public static Individual.Individual FlipBit(Individual.Individual individual)
    {
        var random = new Random();

        var clone = new Individual.Individual(individual);

        var bitIndex = random.Next(clone.Genotype.Count - 1);
        clone.FlipBit(bitIndex);

        return clone;
    }
}