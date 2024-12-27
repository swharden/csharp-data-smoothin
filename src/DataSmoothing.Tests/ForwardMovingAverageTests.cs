namespace DataSmoothing.Tests;

public class ForwardMovingAverageTests
{
    [Test]
    public void Test_ForwardMovingAverage_IsSameAsSlow()
    {
        double[] data = TestData.Continuous;
        double[] smooth1 = MovingAverage.Forward(data, 7);
        double[] smooth2 = MovingAverage.ForwardSlow(data, 7);

        Assert.That(smooth1, Has.Length.EqualTo(smooth2.Length));

        for (int i = 0; i < smooth1.Length; i++)
        {
            Assert.That(smooth1[i], Is.EqualTo(smooth2[i]).Within(1e-10), $"index {i}");
        }
    }

    [Test]
    public void Test_Continuous_ForwardMovingAverage()
    {
        double[] data = TestData.Continuous;
        double[] smooth = MovingAverage.Forward(data, 8);
        Plotting.SaveTestImage("Forward Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_ForwardMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.Forward(data, 8);
        Plotting.SaveTestImage("Forward Moving Average", data, smooth);
    }

    [Test]
    public void Test_Discontinuous_HalvingForwardMovingAverage()
    {
        double[] data = TestData.Discontinuous;
        double[] smooth = MovingAverage.HalvingForward(data, 8);
        Plotting.SaveTestImage("Forward Moving Average", data, smooth);
    }
}
