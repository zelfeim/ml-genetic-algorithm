namespace GeneticAlgorithm.Operators;

public static class GeneticOperators
{
    public static List<Individual> Crossover(Individual firstParent, Individual secondParent, int individualByteSize)
    {
        var firstParentBytes = firstParent.Genotype;
        var secondParentBytes = secondParent.Genotype;

        var random = new Random();

        var byteCutoff = random.Next(individualByteSize - 2);

        var firstChildBytes = firstParentBytes[.. byteCutoff]
            .Concat(secondParentBytes[byteCutoff..(individualByteSize - 1)]).ToArray();
        var secondChildBytes = secondParentBytes[.. byteCutoff]
            .Concat(firstParentBytes[byteCutoff..(individualByteSize - 1)]).ToArray();

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

        var index = random.Next(individualByteSize - 1);

        var child = new Individual(individual);
        child.Genotype[index] ^= 1;

        return child;
    }
}