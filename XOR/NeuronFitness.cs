using GeneticAlgorithm.Fitness;

namespace XOR;

public class NeuronFitness : IFitness
{
    private int Bias = 1;
    
    public Dictionary<Tuple<int, int>, int> xorTable = 
        new()
        {
            {new Tuple<int, int>(0, 0), 0}, 
            {new Tuple<int, int>(0, 1), 1},
            {new Tuple<int, int>(1, 0), 1},
            {new Tuple<int, int>(1, 1), 0}
        };
    
    public double Calculate(List<double> arguments)
    {
        var sum = 0d;
        
        foreach (var xorRow in xorTable)
        {
            var hidden = new double[2];
            for (var i = 0; i < 2; ++i)
            {
                var argumentIndex = i * 3;
                hidden[i] = CalculateNeuron(arguments[argumentIndex], xorRow.Key.Item1, arguments[argumentIndex + 1], xorRow.Key.Item2, arguments[argumentIndex + 2]);
            }
            
            var result = CalculateNeuron(arguments[6], hidden[0], arguments[7], hidden[1], arguments[8]);
            sum += Math.Pow(xorRow.Value - result, 2);
        }
        
        return sum;
    }

    private static double CalculateNeuron(double arg1, double x, double arg2, double y, double arg3)
    {
        return Sigma(arg1 * x + arg2 * y + arg3);
    }

    private static double Sigma(double value)
    {
        return 1 / (1 + Math.Exp(-value));
    }
}