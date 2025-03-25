using System.Collections;
using System.Collections.Generic;
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
        var firstParent = new Individual([1, 0, 1, 0, 1, 1]);
        var secondParent = new Individual([0, 0, 1, 1, 0, 0]);

        var expectedFirstChildBytes = new List<byte>([1, 0, 1, 0, 0, 0 ]);
        var expectedSecondChildBytes = new List<byte>([ 0, 0, 1, 1, 1, 1 ]);

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
       var individual = new Individual([1]);
       var expectedGenotype = new List<byte>([0]);
       
       // Act
       GeneticOperators.Mutate(individual);

       // Assert
       individual.Genotype.Should().BeEquivalentTo(expectedGenotype);
    }
}