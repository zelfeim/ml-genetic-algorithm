using System.Collections;
using GeneticAlgorithm.Helpers;

namespace GeneticAlgorithm;

public static class IndividualFactory
{
    private static readonly Random Random = new();

    public static Individual CreateRandomIndividual(int genotypeLength)
    {
        var bits = new List<byte>(genotypeLength);

        for (var i = 0; i < genotypeLength; i++)
        {
            bits.Add(Convert.ToByte(Random.NextBoolean()));
        }

        return new Individual(bits);
    }

    public static List<Individual> CreateRandomIndividuals(int genotypeLength, int count)
    {
        var individuals = new List<Individual>();

        for (var i = 0; i < count; i++) individuals.Add(CreateRandomIndividual(genotypeLength));

        return individuals;
    }
}