
namespace GeneticAlgorithm.Model;

public class Model
{
    private static int _parameterMinValue;
    private static int _parameterMaxValue;

    private static int _parameterValueRange;

    private static int _chromosomesPerParameter;

    public Model(int parameterMinValue, int parameterMaxValue, int chromosomesPerParameter)
    {
        _parameterMinValue = parameterMinValue;
        _parameterMaxValue = parameterMaxValue;
        _chromosomesPerParameter = chromosomesPerParameter;
        _parameterValueRange = _parameterMaxValue - _parameterMinValue;
    }

    // TODO: To decimal?
    public List<double> Parameters { get; set; } = [];

    public List<byte> EncodeParameter(double parameter)
    {
        var bytes = new List<byte>(_chromosomesPerParameter);

        parameter = Math.Max(parameter, _parameterMinValue);
        parameter = Math.Min(parameter, _parameterMaxValue);

        var ctmp = Math.Round((parameter - _parameterMinValue) / _parameterValueRange *
                              (Math.Pow(2, _chromosomesPerParameter) - 1));

        for (var i = 0; i < _chromosomesPerParameter; i++)
        {
            var value = Math.Floor(ctmp / Math.Pow(2, i) % 2);
            var byteValue = Convert.ToByte(value);
            bytes.Add(byteValue);
        }

        // Reverse bytes because now the smallest byte is at the beginning
        bytes.Reverse();

        return bytes;
    }

    public decimal DecodeChromosomes(List<byte> chromosomes)
    {
        var ctmp = 0.0;

        for (var i = 0; i < _chromosomesPerParameter; i++)
        {
            ctmp += chromosomes[^(i + 1)] * Math.Pow(2, i);
        }

        var calculatedValue =
            _parameterMinValue + ctmp / (Math.Pow(2, _chromosomesPerParameter) - 1) * _parameterValueRange;
        return (decimal)Math.Round(calculatedValue, 2, MidpointRounding.ToZero);
    }
}