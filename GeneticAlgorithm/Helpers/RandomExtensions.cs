namespace GeneticAlgorithm.Helpers;

public static class RandomExtension
{
    public static bool NextBoolean(this Random random)
    {
        return random.Next(2) == 0;
    }
}