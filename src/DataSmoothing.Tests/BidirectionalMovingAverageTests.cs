using ScottPlot;

namespace DataSmoothing.Tests;

internal class BidirectionalMovingAverageTests
{
    [Test]
    public void Test_Continuous_BidirectionalMovingAverage()
    {
        double[] data = TestData.Continuous;
        double[] smooth = MovingAverage.Bidirectional(data, 8);
        Plotting.PlotOriginalVsSmooth("Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_BidirectionalMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.Bidirectional(data, 8);
        Plotting.PlotOriginalVsSmooth("Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_HalvingBidirectionalMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.HalvingBidirectional(data, 8);
        Plotting.PlotOriginalVsSmooth("Halving Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_HalvingBidirectionalMovingAverageVsConvolution()
    {
        double[] data = TestData.Discontinuous;
        double[] smoothHalving = MovingAverage.HalvingBidirectional(data, 8);
        double[] smoothGaussian = Convolution.Smooth(data, 24);
        double[] xs = ScottPlot.Generate.Consecutive(data.Length);

        Plot plot = new();
        var points = plot.Add.ScatterPoints(xs, data);
        points.LegendText = "Raw Data";

        var line1 = plot.Add.ScatterLine(xs, smoothHalving);
        line1.LineWidth = 2;
        line1.LegendText = "HBSMA";

        var line2 = plot.Add.ScatterLine(xs, smoothGaussian);
        line2.LineWidth = 2;
        line2.LineColor = Colors.Black;
        line2.LinePattern = LinePattern.Dotted;
        line2.LegendText = "Gaussian";

        plot.Title("Halving Bidirectional SMA vs. Gaussian Smoothing");
        plot.ShowLegend(ScottPlot.Alignment.UpperLeft);
        Plotting.SaveTestImage(plot);
    }
}
