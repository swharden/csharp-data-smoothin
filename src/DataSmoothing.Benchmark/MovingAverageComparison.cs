/*
| Method                    | Mean        | Error    | StdDev   |
|-------------------------- |------------:|---------:|---------:|
| MovingAverage_Forward     |    19.64 us | 0.335 us | 0.329 us |
| MovingAverage_ForwardSlow | 2,347.68 us | 8.438 us | 7.892 us |
*/

using BenchmarkDotNet.Attributes;

namespace DataSmoothing.Benchmark;

public class MovingAverageComparison
{
    readonly double[] Data = new ScottPlot.RandomDataGenerator(0).RandomWalk(10_000);

    [Benchmark]
    public double[] MovingAverage_Forward() => DataSmoothing.MovingAverage.Forward(Data, 50);
    [Benchmark]
    public double[] MovingAverage_ForwardSlow() => DataSmoothing.MovingAverage.ForwardSlow(Data, 50);
}
