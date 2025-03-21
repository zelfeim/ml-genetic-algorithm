using System.Collections.Immutable;

namespace GeneticAlgorithm.Model;

public class Model
{
    public static float ParameterMinValue = -1;
    public static float ParameterMaxValue = 2;

    public static float ParameterValueRange;

    public static int ChromosomesPerParameter = 3;

    public Model(float parameterMinValue, float parameterMaxValue, int chromosomesPerParameter)
    {
        ParameterMinValue = parameterMinValue;
        ParameterMaxValue = parameterMaxValue;
        ChromosomesPerParameter = chromosomesPerParameter;
        ParameterValueRange = ParameterMaxValue - ParameterMinValue;
    }
        
    public ImmutableArray<Parameter> Parameters { get; private set; }
    
    // TODO: in static method?
    public List<byte> EncodeParameter(float parameter)
    {
        var bytes = new List<byte>();
        
        var zd = ParameterMaxValue - ParameterMinValue;
        
        var ctmp = Math.Round((parameter - ParameterMinValue) / zd * (Math.Pow(2, ChromosomesPerParameter) - 1));
        
        for (int i = 0; i < ChromosomesPerParameter - 1; i++)
        {
            var byteValue = Math.Floor(ctmp/Math.Pow(2, i)) % 2;
        }
        
        return [];
    }

    public static double DecodeChromosomes(List<byte> chromosomes)
    {
        var ctmp = 0d;

        for (var i = 0; i < ChromosomesPerParameter; i++)
        {   
            ctmp += chromosomes[^(i + 1)] * Math.Pow(2, i);
        }

        return Math.Round(ParameterMinValue + ctmp / (Math.Pow(2, ChromosomesPerParameter) - 1) * ParameterValueRange, 2);
    }
}