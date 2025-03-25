using System.Collections;
using FluentAssertions;
using GeneticAlgorithm.Operators;
using Xunit;

namespace GeneticAlgorithm.Tests.Operators;

public class GeneticOperatorsTest
{
    [Fact]
    public void Crossover_Should_Return_Expected_Genotypes()
    {
        // Arrange
        var firstParent = new Individual([true, false, true, false, true, true]);
        var secondParent = new Individual([false, false, true, true, false, false]);

        var expectedFirstChildBytes = new BitArray([true, false, true, false, false, false ]);
        var expectedSecondChildBytes = new BitArray([ false, false, true, true, true, true ]);

        // Act
        var children = GeneticOperators.Crossover(firstParent, secondParent, 4);

        // Assert
        children[0].Genotype.Should().BeEquivalentTo(expectedFirstChildBytes);
        children[1].Genotype.Should().BeEquivalentTo(expectedSecondChildBytes);
    }

    [Fact]
    public void Mutate_Should_Flip_Individual_Bit()
    {
       // Arrange 
       var individual = new Individual([true]);
       var expectedGenotype = new BitArray([false]);
       
       // Act
       GeneticOperators.Mutate(individual);

       // Assert
       individual.Genotype.Should().BeEquivalentTo(expectedGenotype);
    }
}