namespace GeneticAlgorithm.Model;

public class Parameter
{
    public readonly int ChromosomesCount;
    public readonly int MaxValue;
    public readonly int MinValue;

    public Parameter(int minValue, int maxValue, int chromosomesCount)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        ChromosomesCount = chromosomesCount;

        ParameterRepresentation = CreateParameterRepresentation();
    }

    public Dictionary<List<byte>, double> ParameterRepresentation { get; }

    public double Value { get; set; }

    public int ValueRange => MaxValue - MinValue;

    private Dictionary<List<byte>, double> CreateParameterRepresentation()
    {
        var representation = new Dictionary<List<byte>, double>();

        var chromosomeCombinations = Math.Pow(2, ChromosomesCount);

        for (var i = 0; i < chromosomeCombinations; i++)
        {
            var ctmp = 0.0d;

            var bits = Enumerable.Range(0, ChromosomesCount).Select(b => (byte)((i >> b) & 1)).ToList();

            for (var j = 0; j < ChromosomesCount; j++) ctmp += Convert.ToInt32(bits[j]) * Math.Pow(2, j);

            var value = MinValue + ctmp / (Math.Pow(2, ChromosomesCount) - 1) * ValueRange;

            representation.Add(bits, value);
        }

        return representation;
    }
}