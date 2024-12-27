namespace DataSmoothing.Tests;

internal class BidirectionalMovingAverageTests
{
    [Test]
    public void Test_Continuous_BidirectionalMovingAverage()
    {
        double[] data = TestData.Continuous;
        double[] smooth = MovingAverage.Bidirectional(data, 8);
        Plotting.SaveTestImage("Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_BidirectionalMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.Bidirectional(data, 8);
        Plotting.SaveTestImage("Bidirectional Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_HalvingBidirectionalMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.HalvingBidirectional(data, 8);
        Plotting.SaveTestImage("Halving Bidirectional Moving Average", data, smooth);
    }
}
