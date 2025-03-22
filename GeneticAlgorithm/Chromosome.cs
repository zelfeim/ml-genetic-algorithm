namespace GeneticAlgorithm;

public class Chromosome
{
    public Chromosome(List<byte> bytes)
    {
        Value = bytes;
    }

    public Chromosome(byte[] bytes)
    {
        Value = bytes.ToList();
    }
    
    public List<byte> Value { get; set; }
}