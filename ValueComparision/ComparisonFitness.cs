using GeneticAlgorithm.Fitness;

namespace ValueComparision;

public class ComparisionFitness(Dictionary<double, double> functionValues) : IFitness
{
    private Dictionary<double, double> FunctionValues { get; } = functionValues;

    public double Calculate(List<double> arguments)
    {
        var pa = arguments[0];
        var pb = arguments[1];
        var pc = arguments[2];

        return FunctionValues.Keys.Aggregate(0d, (sum, step) =>
        {
            var result = pa * Math.Sin(pb * step + pc);

            return sum + Math.Pow(FunctionValues[step] - result, 2);
        });
    }
}