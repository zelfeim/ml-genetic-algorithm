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

        var expectedFirstChildBytes = new byte[] { 1, 0, 1, 0, 0, 0 };
        var expectedSecondChildBytes = new byte[] { 0, 0, 1, 1, 1, 1 };
        
        // Act
        var children = GeneticOperators.Crossover(firstParent, secondParent, 3);

        // Assert
        children[0].Chromosomes.Should().BeEquivalentTo(expectedFirstChildBytes);
        children[1].Chromosomes.Should().BeEquivalentTo(expectedSecondChildBytes);
    }
}