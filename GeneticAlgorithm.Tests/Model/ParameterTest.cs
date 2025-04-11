using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GeneticAlgorithm.Model;
using Xunit;

namespace GeneticAlgorithm.Tests.Model;

public class ParameterTest
{
    [Fact]
    public void Parameter_Should_Create_Expected_BitArray_Value_Dictionary()
    {
        // Arrange 
        var expectedDictionary = new Dictionary<BitArray, double>
        {
            {
                new BitArray([0]), -1.00d
            },
            {
                new BitArray([1]), -0.57d
            },
            {
                new BitArray([2]), -0.14d
            },
            {
                new BitArray([3]), 0.28d
            },
            {
                new BitArray([4]), 0.71d
            },
            {
                new BitArray([5]), 1.14d
            },
            {
                new BitArray([6]), 1.57d
            },
            {
                new BitArray([7]), 2.00d
            }
        };

        // Act
        var parameter = new Parameter(-1, 2, 3);

        // Assert
        parameter.ParameterRepresentation.Keys.Should().BeEquivalentTo(expectedDictionary.Keys);
        parameter.ParameterRepresentation.Values.ElementAt(0).Should()
            .BeApproximately(expectedDictionary.Values.ElementAt(0), 0.01);
        parameter.ParameterRepresentation.Values.ElementAt(2).Should()
            .BeApproximately(expectedDictionary.Values.ElementAt(2), 0.01);
        parameter.ParameterRepresentation.Values.ElementAt(5).Should()
            .BeApproximately(expectedDictionary.Values.ElementAt(5), 0.01);
        parameter.ParameterRepresentation.Values.ElementAt(7).Should()
            .BeApproximately(expectedDictionary.Values.ElementAt(7), 0.01);
    }
}