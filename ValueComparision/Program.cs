const int chromosomeCount = 4;
const int iterations = 500;
const int populationSize = 13;
const int tournamentSize = 3;

var solution = new ValueComparision.Solution(chromosomeCount, iterations, populationSize, tournamentSize);
solution.Run();

return 0;
