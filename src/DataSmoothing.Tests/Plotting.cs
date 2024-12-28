using ScottPlot;

namespace DataSmoothing.Tests;

internal static class Plotting
{
    public static void PlotOriginalVsSmooth(string title, double[] data, double[] smooth)
    {
        ScottPlot.Plot plot = new();
        double[] xs = ScottPlot.Generate.Consecutive(data.Length);
        var points = plot.Add.ScatterPoints(xs, data);
        points.LegendText = "Raw Data";

        var line = plot.Add.ScatterLine(xs, smooth);
        line.LineWidth = 2;
        line.LegendText = "Smooth";

        plot.ShowLegend(ScottPlot.Alignment.LowerRight);
        plot.Title(title);

        string callerName = new System.Diagnostics.StackTrace().GetFrame(1)!.GetMethod()!.Name;
        SaveTestImage(plot, callerName);
    }

    public static void SaveTestImage(Plot plot, string? callerName = null)
    {
        if (callerName is null)
        {
            callerName = new System.Diagnostics.StackTrace().GetFrame(1)!.GetMethod()!.Name;
        }

        string outputFolder = "TestImages";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        plot.SavePng(Path.Join(outputFolder, $"{callerName}.png"), 500, 300).ConsoleWritePath();
    }
}
