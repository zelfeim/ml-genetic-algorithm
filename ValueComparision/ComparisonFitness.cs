using GeneticAlgorithm.Fitness;

namespace ValueComparision;

public class ComparisionFitness(Dictionary<decimal, float> functionValues) : IFitness
{
    private Dictionary<decimal, float> FunctionValues { get; set; } = functionValues;

    public double Calculate(List<double> arguments)
    {
        var pa = arguments[0];
        var pb = arguments[1];
        var pc = arguments[2];

        var sum = 0d;
        foreach (var step in FunctionValues.Keys)
        {
            var result = pa * Math.Sin(pb * (double)step * pc);

            sum += Math.Pow(FunctionValues[step] - result, 2);
        }

        return sum;
    }
}