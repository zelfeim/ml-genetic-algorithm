using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace XOR.GUI.ViewModels;

public class GraphViewModel
{
    public ISeries[] Series { get; set; }
    public Axis XAxes { get; }
    public Axis YAxes { get; }
    
    public GraphViewModel()
    {
        XAxes = new Axis()
        {
            Name = "X",
        };
        YAxes = new Axis()
        {
            Name = "Y"
        };
        
        var bestSeries = new LineSeries<double>
        {
            Name = "Best Fitness",
            Values = MainWindowViewModel._helperDictionary.Values.Select(x => x.Item1).ToArray()
        };

        var averageSeries = new LineSeries<double>
        {
            Name = "Average Fitness",
            Values = MainWindowViewModel._helperDictionary.Values.Select(x => x.Item2).ToArray()
        };
        
        Series = [bestSeries, averageSeries];
    }
}