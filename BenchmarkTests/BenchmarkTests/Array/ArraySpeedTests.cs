using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[RPlotExporter, RankColumn]
public class ArraySpeedTests
{
    private byte[] array;
    private List<byte> list;
    private IEnumerable<byte> enumerable;

    [Params(1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        array = new byte[N];
        new Random(42).NextBytes(array);

        list = new List<byte>(array);
        enumerable = list;
    }

    [Benchmark]
    public int ForArray()
    {
        var summ = 0;
        for (int i = 0; i < array.Length; i++)
        {
            summ += array[i];
        }
        return summ;
    }

    [Benchmark]
    public int ForeachArray()
    {
        var summ = 0;
        foreach (var item in array)
        {
            summ += item;
        }
        return summ;
    }

    [Benchmark]
    public int ForList()
    {
        var summ = 0;
        for (int i = 0; i < list.Count; i++)
        {
            summ += list[i];
        }
        return summ;
    }

    [Benchmark]
    public int ForeachList()
    {
        var summ = 0;
        foreach (var item in list)
        {
            summ += item;
        }
        return summ;
    }

    [Benchmark]
    public int ForeachEnumerable()
    {
        var summ = 0;
        foreach (var item in enumerable)
        {
            summ += item;
        }
        return summ;
    }
}