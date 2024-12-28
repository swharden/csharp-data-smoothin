namespace DataSmoothing;

public static class Convolution
{
    public static double[] Smooth(double[] data, int windowSize)
    {
        double[] kernel = Hanning(windowSize);
        return Convolve(data, kernel);
    }

    public static double[] Convolve(double[] data, double[] kernel)
    {
        int dataSize = data.Length;
        int kernelSize = kernel.Length;
        int halfKernel = kernelSize / 2;
        double[] smoothedData = new double[dataSize];

        for (int i = 0; i < dataSize; i++)
        {
            double sum = 0;
            double weightSum = 0;

            for (int j = 0; j < kernelSize; j++)
            {
                int dataIndex = i + j - halfKernel;
                if (dataIndex >= 0 && dataIndex < dataSize)
                {
                    sum += data[dataIndex] * kernel[j];
                    weightSum += kernel[j];
                }
            }

            smoothedData[i] = weightSum > 0 ? sum / weightSum : 0;
        }

        return smoothedData;
    }

    public static double[] Hanning(int windowSize)
    {
        return Enumerable
            .Range(0, windowSize)
            .Select(i => 0.5 * (1 - Math.Cos((2 * Math.PI * i) / (windowSize - 1))))
            .ToArray();
    }
}
