namespace GeneticAlgorithm.Operators;

public static class GeneticOperators
{
    // Can also implement with random values (for example exchange 1, 2, 6 byte) other than cutoff
    public static List<Individual> Crossover(Individual firstParent, Individual secondParent, int cutoff = 0)
    {
        if (firstParent.Chromosomes.Count != secondParent.Chromosomes.Count)
        {
            throw new Exception("Genotype sizes are not the same!");
        }

        var genotypeSize = firstParent.Chromosomes.Count;
        
        var firstParentBytes = firstParent.Genotype;
        var secondParentBytes = secondParent.Genotype;

        var random = new Random();
        var byteCutoff = cutoff != 0 ? cutoff : random.Next(genotypeSize - 1);

        var firstChildBytes = firstParentBytes[.. byteCutoff]
            .Concat(secondParentBytes[byteCutoff..(genotypeSize)]).ToList();
        var secondChildBytes = secondParentBytes[.. byteCutoff]
            .Concat(firstParentBytes[byteCutoff..(genotypeSize)]).ToList();

        var children = new List<Individual>
        {
            new(firstChildBytes),
            new(secondChildBytes)
        };

        return children;
    }

    public static Individual Mutate(Individual individual, int individualByteSize)
    {
        var random = new Random();

        var chromosomeIndex = random.Next(individual.Chromosomes.Count - 1);
        var byteIndex = random.Next(individualByteSize - 1);

        var child = new Individual(individual);
        child.Chromosomes[chromosomeIndex].Value[byteIndex] ^= 1;

        return child;
    }
}