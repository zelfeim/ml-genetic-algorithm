const int chromosomeCount = 3;
const int iterations = 500;
const int populationSize = 9;
const int tournamentSize = 2;

var solution = new MutatedRug.Solution(chromosomeCount, iterations, populationSize, tournamentSize);
solution.Run();

return 0;
