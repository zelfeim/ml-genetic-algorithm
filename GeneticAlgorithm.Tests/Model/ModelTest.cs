using System.Collections.Generic;
using FluentAssertions;
using GeneticAlgorithm.Model;
using Xunit;

namespace GeneticAlgorithm.Tests.Model;

public class ModelTest
{

    [Fact]
    public void DecodeChromosomes_Should_Return_Expected_Parameter_Values()
    {
        // Arrange 
        var model = new GeneticAlgorithm.Model.Model(-1, 2, 3);

        // Act
        var parameter = GeneticAlgorithm.Model.Model.DecodeChromosomes([1, 1, 0]);

        // Assert
        parameter.Should().Be(1.57);
    }
}