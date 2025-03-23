namespace GeneticAlgorithm;

public class Individual
{
    public Individual(Individual other)
    {
        Chromosomes = other.Chromosomes;
    }

    public Individual(List<Chromosome> chromosomes)
    {
        Chromosomes = chromosomes;
    }

    public Individual(List<byte> genotype)
    {
        // TODO: Get ChromosomesPerParameter from config
        Chromosomes = genotype.Chunk(3).Select(b => new Chromosome(b)).ToList();
    }
    
    // Every parameter can have different lengths of chromosomes
    // Chromosome represents 1 value (byte)
    public List<Chromosome> Chromosomes {get; set;}

    public List<byte> Genotype => Chromosomes.SelectMany(c => c.Value).ToList<byte>();

    public double Fitness { get; private set; }
}