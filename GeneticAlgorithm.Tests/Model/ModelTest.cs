using System.Collections.Generic;
using FluentAssertions;
using GeneticAlgorithm.Model;
using Xunit;

namespace GeneticAlgorithm.Tests.Model;

public class ModelTest
{
    private const int ParameterMinValue = -1;
    private const int ParameterMaxValue = 2;

    private const int ChromosomesPerParameter = 3;

    public static IEnumerable<object[]> TestData() =>
        new List<object[]>
        {
            // new object[] { -1.00M, new List<byte> { 0, 0, 0 } },
            new object[] { -0.57M, new List<byte> { 0, 0, 1 } },
            new object[] { -0.14M, new List<byte> { 0, 1, 0 } },
            new object[] { 0.28M, new List<byte> { 0, 1, 1 } },
            new object[] { 0.71M, new List<byte> { 1, 0, 0 } },
            new object[] { 1.14M, new List<byte> { 1, 0, 1 } },
            new object[] { 1.57M, new List<byte> { 1, 1, 0 } },
            new object[] { 2.00M, new List<byte> { 1, 1, 1 } },
        };
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void EncodeParameters_Should_Return_Expected_Chromosomes(float parameterValue, List<byte> expectedBytes)
    {
        // Arrange
        var model = TestModel;

        // Act
        var chromosomes = GeneticAlgorithm.Model.Model.EncodeParameter(parameterValue);

        // Assert
        chromosomes.Should().BeEquivalentTo(expectedBytes);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void DecodeChromosomes_Should_Return_Expected_Parameter_Values(decimal expectedParameterValue, List<byte> bytes)
    {
        // Arrange 
        var model = TestModel;

        // Act
        var parameter = GeneticAlgorithm.Model.Model.DecodeChromosomes(bytes);

        // Assert
        parameter.Should().Be(expectedParameterValue);
    }

    private static GeneticAlgorithm.Model.Model TestModel =>
        new(ParameterMinValue, ParameterMaxValue, ChromosomesPerParameter);
}