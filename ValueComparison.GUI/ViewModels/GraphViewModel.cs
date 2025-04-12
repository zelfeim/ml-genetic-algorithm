using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace ValueComparison.GUI.ViewModels;

public class GraphViewModel
{
    public ISeries[] Series { get; set; }

    public GraphViewModel()
    {
        Series = [];
    }
    
    public GraphViewModel(Dictionary<int, Tuple<double, double>> valueHistory)
    {
        var bestSeries = new LineSeries<double>
        {
            Name = "Best Fitness",
            Values = valueHistory.Values.Select(x => x.Item1).ToArray()
        };

        var averageSeries = new LineSeries<double>
        {
            Name = "Average Fitness",
            Values = valueHistory.Values.Select(x => x.Item2).ToArray()
        };
        
        Series = [bestSeries, averageSeries];
    }
}