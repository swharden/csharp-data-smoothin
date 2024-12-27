using ScottPlot.TickGenerators.TimeUnits;

namespace DataSmoothing.Tests;

public class MovingAverageTests
{
    [Test]
    public void Test_ForwardMovingAverage()
    {
        double[] data = new ScottPlot.RandomDataGenerator(0).RandomWalk(50);
        double[] smooth = MovingAverage.Forward(data, 7);
        Plotting.SaveTestImage("Forward Moving Average", data, smooth);
    }

    [Test]
    public void Test_BidirectionalMovingAverage()
    {
        double[] data = new ScottPlot.RandomDataGenerator(0).RandomWalk(50);
        double[] smooth = MovingAverage.Bidirectional(data, 7);
        Plotting.SaveTestImage("Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_ForwardMovingAverage_IsSameAsSlow()
    {
        double[] data = new ScottPlot.RandomDataGenerator(0).RandomWalk(50);
        double[] smooth1 = MovingAverage.Forward(data, 7);
        double[] smooth2 = MovingAverage.ForwardSlow(data, 7);

        Assert.That(smooth1, Has.Length.EqualTo(smooth2.Length));

        for (int i = 0; i < smooth1.Length; i++)
        {
            Assert.That(smooth1[i], Is.EqualTo(smooth2[i]).Within(1e-10), $"index {i}");
        }
    }
}
