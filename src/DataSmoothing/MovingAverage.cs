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

    public static double[] HalvingForward(double[] data, int windowSize)
    {
        double[] smooth = data;
        while (windowSize > 0)
        {
            smooth = Forward(smooth, windowSize);
            windowSize /= 2;
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

    public static double[] Bidirectional(double[] data, int windowSize)
    {
        double[] smooth = new double[data.Length];

        // smooth from left to right
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

        // smooth from right to left
        runningSum = 0;
        pointsInSum = 0;
        for (int i = smooth.Length - 1; i >= 0; i--)
        {
            runningSum += data[i];

            if (pointsInSum < windowSize)
            {
                pointsInSum++;
                smooth[i] += runningSum / pointsInSum;
                continue;
            }

            runningSum -= data[i + windowSize];
            smooth[i] += runningSum / windowSize;
        }

        // average the two directions
        for (int i = 0; i < smooth.Length; i++)
        {
            smooth[i] /= 2;
        }

        return smooth;
    }

    public static double[] HalvingBidirectional(double[] data, int windowSize)
    {
        double[] smooth = data;
        while (windowSize > 0)
        {
            smooth = Bidirectional(smooth, windowSize);
            windowSize /= 2;
        }
        return smooth;
    }
}
