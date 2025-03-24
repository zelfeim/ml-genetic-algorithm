using System.Collections;

namespace GeneticAlgorithm.Model;

public class Parameter
{
    public readonly int ChromosomesCount;
    public readonly int MaxValue;
    public readonly int MinValue;

    public readonly Dictionary<BitArray, double> ParameterRepresentation;

    public Parameter(int minValue, int maxValue, int chromosomesCount)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        ChromosomesCount = chromosomesCount;

        ParameterRepresentation = CreateParameterRepresentation();
    }

    public int ValueRange => MaxValue - MinValue;

    private Dictionary<BitArray, double> CreateParameterRepresentation()
    {
        var representation = new Dictionary<BitArray, double>();

        var chromosomeCombinations = Math.Pow(2, ChromosomesCount);

        for (var i = 0; i < chromosomeCombinations; i++)
        {
            var ctmp = 0.0d;
            var bitArray = new BitArray([i]);

            for (var j = 0; j < ChromosomesCount; j++)
            {
                ctmp += Convert.ToInt32(bitArray[j]) * Math.Pow(2, j);
            }
            
            var value = MinValue + ctmp / (Math.Pow(2, ChromosomesCount) - 1) * ValueRange;

            representation.Add(bitArray, value);
        }

        return representation;
    }
}