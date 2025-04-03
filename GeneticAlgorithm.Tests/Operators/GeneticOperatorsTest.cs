using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using GeneticAlgorithm.Operators;
using Xunit;

namespace GeneticAlgorithm.Tests.Operators;

public class MutationTest
{
    [Fact]
    public void Crossover_Should_Return_Expected_Genotypes()
    {
        // Arrange
        var firstParent = new Individual.Individual([1, 0, 1, 0, 1, 1]);
        var secondParent = new Individual.Individual([0, 0, 1, 1, 0, 0]);

        var expectedFirstChildBytes = new List<byte>([1, 0, 1, 0, 0, 0 ]);
        var expectedSecondChildBytes = new List<byte>([ 0, 0, 1, 1, 1, 1 ]);

        // Act
        var children = Mutation.Crossover(firstParent, secondParent, 4);

        // Assert
        children[0].Genotype.Should().BeEquivalentTo(expectedFirstChildBytes);
        children[1].Genotype.Should().BeEquivalentTo(expectedSecondChildBytes);
    }

    [Fact]
    public void Mutate_Should_Flip_Individual_Bit()
    {
       // Arrange 
       var individual = new Individual.Individual([1]);
       var expectedGenotype = new List<byte>([0]);
       
       // Act
       Mutation.FlipBit(individual);

       // Assert
       individual.Genotype.Should().BeEquivalentTo(expectedGenotype);
    }
}