using System.Collections;

namespace GeneticAlgorithm.Model;

public class Model
{
    public Model()
    {
    }

    public List<Parameter> Parameters { get; set; }

    public List<double> DecodeGenotype(Individual individual)
    {
        var values = new List<double>();

        var bits = individual.Genotype;

        var genotypeIndex = 0;
        foreach (var parameter in Parameters)
        {
            var parameterBits = bits.Skip(genotypeIndex).Take(parameter.ChromosomesCount);
            
            genotypeIndex += parameter.ChromosomesCount;
            values.Add(parameter.ParameterRepresentation.FirstOrDefault(kvp => kvp.Key.SequenceEqual(parameterBits)).Value);
        }

        return values;
    }
}