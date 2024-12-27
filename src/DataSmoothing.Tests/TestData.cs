namespace DataSmoothing.Tests;

public static class TestData
{
    public static readonly double[] Continuous = GetContinuousData(75);
    public static readonly double[] Discontinuous = GetBrokenData(75);

    private static double[] GetContinuousData(int count)
    {
        return new ScottPlot.RandomDataGenerator(0).RandomWalk(count);
    }

    private static double[] GetBrokenData(int count, double shift = 20)
    {
        double[] data = GetContinuousData(count);
        int breakIndex1 = data.Length / 3;
        int breakIndex2 = breakIndex1 * 2;
        for (int i = breakIndex1; i < breakIndex2; i++)
            data[i] += shift;
        return data;
    }
}
