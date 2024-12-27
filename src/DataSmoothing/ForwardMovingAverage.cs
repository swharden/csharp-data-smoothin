namespace DataSmoothing;

public static class MovingAverage
{
    public static double[] Forward(double[] data, int windowSize)
    {
        double[] smooth = new double[data.Length];

        double runningSum = 0;
        int pointsInSum = 0;
        for (int i = 0; i < smooth.Length; i++)
        {
            runningSum += data[i];

            if (pointsInSum < windowSize)
            {
                pointsInSum++;
                smooth[i] += runningSum / pointsInSum;
                continue;
            }

            runningSum -= data[i - windowSize];
            smooth[i] += runningSum / windowSize;
        }

        return smooth;
    }

    public static double[] ForwardSlow(double[] data, int windowSize)
    {
        double[] smooth = new double[data.Length];

        for (int i = 0; i < smooth.Length; i++)
        {
            if (i < windowSize)
            {
                smooth[i] = data.Take(i + 1).Average();
            }
            else
            {
                smooth[i] = data.Skip(i - windowSize + 1).Take(windowSize).Average();
            }
        }

        return smooth;
    }

    public static double[] Bidirectional(double[] ys, int pointCount = 5)
    {
        double[] smooth = new double[ys.Length];

        // smooth from left to right
        double runningSum = 0;
        int pointsInSum = 0;
        for (int i = 0; i < smooth.Length; i++)
        {
            runningSum += ys[i];

            if (pointsInSum < pointCount)
            {
                pointsInSum++;
                smooth[i] += runningSum / pointsInSum;
                continue;
            }

            runningSum -= ys[i - pointCount];
            smooth[i] += runningSum / pointCount;
        }

        // smooth from right to left
        runningSum = 0;
        pointsInSum = 0;
        for (int i = smooth.Length - 1; i >= 0; i--)
        {
            runningSum += ys[i];

            if (pointsInSum < pointCount)
            {
                pointsInSum++;
                smooth[i] += runningSum / pointsInSum;
                continue;
            }

            runningSum -= ys[i + pointCount];
            smooth[i] += runningSum / pointCount;
        }

        // average the two directions
        for (int i = 0; i < smooth.Length; i++)
        {
            smooth[i] /= 2;
        }

        return smooth;
    }
}
