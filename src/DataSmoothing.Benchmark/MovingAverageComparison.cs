/*

### 50 points, window size of 8 (and 24)

| Method                    | Mean        | Error     | StdDev    |
|-------------------------- |------------:|----------:|----------:|
| MovingAverage_Forward     |    97.29 ns |  0.992 ns |  0.928 ns |
| MovingAverage_ForwardSlow | 2,537.54 ns | 27.904 ns | 26.101 ns |
| MovingAverage_Convolution |   934.83 ns |  5.386 ns |  4.775 ns | (10x faster)

### 10_000 points, window size of 100 (and 300)

| Method                    | Mean        | Error     | StdDev    |
|-------------------------- |------------:|----------:|----------:|
| MovingAverage_Forward     |    19.81 us |  0.114 us |  0.089 us |
| MovingAverage_ForwardSlow | 4,390.91 us | 25.379 us | 23.739 us |
| MovingAverage_Convolution | 2,612.27 us | 12.911 us | 12.077 us | (132x faster)

*/

using BenchmarkDotNet.Attributes;

namespace DataSmoothing.Benchmark;

public class MovingAverageComparison
{
    readonly double[] Data = new ScottPlot.RandomDataGenerator(0).RandomWalk(10_000);

    const int WINDOW_SIZE = 100;

    readonly double[] KERNEL = Convolution.Hanning(WINDOW_SIZE * 3); // 3x achieves similar amount of smoothing as HBSMA

    [Benchmark]
    public double[] MovingAverage_Forward() => DataSmoothing.MovingAverage.Forward(Data, WINDOW_SIZE);

    [Benchmark]
    public double[] MovingAverage_ForwardSlow() => DataSmoothing.MovingAverage.ForwardSlow(Data, WINDOW_SIZE);

    [Benchmark]
    public double[] MovingAverage_Convolution() => DataSmoothing.Convolution.Convolve(Data, KERNEL);
}
