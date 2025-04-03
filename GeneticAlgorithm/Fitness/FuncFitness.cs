namespace GeneticAlgorithm.Fitness;

public class FuncFitness(Func<List<double>, double> funcFitness) : IFitness
{
    public double Calculate(List<double> arguments)
    {
        return funcFitness(arguments);
    }
}