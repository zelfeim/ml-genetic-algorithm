namespace GeneticAlgorithm.Helpers;

public static class GeneRandomizer
{
    private static readonly Random Random = new();

    public static List<byte> GenerateGenotype(int length)
    {
        var genotype = new List<byte>();

        for (var i = 0; i < length; i++)
        {
            var bit = Convert.ToByte(Random.Next(0, 2) == 0);

            genotype.Add(bit);
        }

        return genotype;
    }
}