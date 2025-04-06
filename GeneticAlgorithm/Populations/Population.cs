using GeneticAlgorithm.Helpers;

namespace GeneticAlgorithm.Populations;

public class Population
{
    public int GenerationsCount;
    
    public readonly int ChromosomesCount;
    public readonly int GeneCount;

    private readonly int _populationSize;
    private readonly int _minValue;
    private readonly int _maxValue;
    
    public List<Individual.Individual> Individuals = [];
    
    public readonly Dictionary<List<byte>, double> GenotypeLookupTable;

    private readonly Func<List<Individual.Individual>, Individual.Individual> _selectBestFunc;

    public Population(int populationSize, int generationsCount, int chromosomesCount, int geneCount, int minValue, int maxValue, Func<List<Individual.Individual>, Individual.Individual> selectBestFunc)
    {
        _populationSize = populationSize;
        GenerationsCount = generationsCount;
        ChromosomesCount = chromosomesCount;
        GeneCount = geneCount;
        _minValue = minValue;
        _maxValue = maxValue;
        _selectBestFunc = selectBestFunc;

        GenotypeLookupTable = CreateLookupTable();
    }

    public void CreateInitialGeneration()
    {
        for (var i = 0; i < _populationSize; i++)
        {
            var bits = GeneRandomizer.GenerateGenotype(ChromosomesCount * GeneCount);
            Individuals.Add(new Individual.Individual(bits));
        }
    }

    public void CreateNewGeneration(List<List<byte>> genotypes)
    {
        Individuals = genotypes.Select(g => new Individual.Individual(g)).ToList();
    }

    public void CreateNewGeneration(List<Individual.Individual> individuals)
    {
        Individuals = individuals.ToList();
        GenerationsCount--;
    }
    
    private Dictionary<List<byte>, double> CreateLookupTable()
    {
        var representation = new Dictionary<List<byte>, double>();

        var chromosomeCombinations = Math.Pow(2, ChromosomesCount);

        for (var i = 0; i < chromosomeCombinations; i++)
        {
            var ctmp = 0.0d;

            var bits = Enumerable.Range(0, ChromosomesCount).Select(b => (byte)((i >> b) & 1)).ToList();

            for (var j = 0; j < ChromosomesCount; j++)
            {
                ctmp += Convert.ToInt32(bits[j]) * Math.Pow(2, j);
            }
            
            var value = _minValue + ctmp / (Math.Pow(2, ChromosomesCount) - 1) * (_maxValue - _minValue);

            representation.Add(bits, value);
        }

        return representation;
    }

    public Individual.Individual GetBestIndividual()
    {
        return _selectBestFunc(Individuals);
    }
}