using GeneticAlgorithm.Fitness;

namespace XOR;

public class NeuronFitness : IFitness
{
    private readonly Dictionary<Tuple<int, int>, int> _xorTable = 
        new()
        {
            {new Tuple<int, int>(0, 0), 0}, 
            {new Tuple<int, int>(0, 1), 1},
            {new Tuple<int, int>(1, 0), 1},
            {new Tuple<int, int>(1, 1), 0}
        };
    
    public double Calculate(List<double> arguments)
    {
        var sum = 0.0d;
        foreach(var xorRow in _xorTable) 
        {
            var firstNeuronResult = 
                CalculateNeuron(arguments[0], xorRow.Key.Item1, arguments[1], xorRow.Key.Item2, arguments[2]);
            var secondNeuronResult = 
                CalculateNeuron(arguments[3], xorRow.Key.Item1, arguments[4], xorRow.Key.Item2, arguments[5]);
            
            var result = CalculateNeuron(arguments[6], firstNeuronResult, arguments[7], secondNeuronResult, arguments[8]);
            
            sum += Math.Pow(xorRow.Value - result, 2);
        }
        
        return sum;
    }

    private static double CalculateNeuron(double bias1, double x, double bias2, double y, double bias3)
    {
        return Sigmoid(bias1 * x + bias2 * y + bias3);
    }

    private static double Sigmoid(double value)
    {
        return 1 / (1 + Math.Exp(-value));
    }
}