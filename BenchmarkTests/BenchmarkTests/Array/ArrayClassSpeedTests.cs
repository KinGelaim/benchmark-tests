using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[RPlotExporter, RankColumn]
public class ArrayClassSpeedTests
{
    private TestClass[] array;
    private List<TestClass> list;
    private IEnumerable<TestClass> enumerable;

    [Params(1, 10, 100, 1000, 10000, 100000, 1000000, 10000000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        var rnd = new Random(42);

        array = new TestClass[N];
        for (int i = 0; i < N; i++)
        {
            var rndValue = rnd.Next(-100000, 100000);
            var testClass = new TestClass()
            {
                Value = rndValue,
                Text = i.ToString(),
                List = new List<int> { i, i - rndValue, i + rndValue, rndValue }
            };
            array[i] = testClass;
        }

        list = new List<TestClass>(array);
        enumerable = list;
    }

    [Benchmark]
    public int ForArray()
    {
        var summ = 0;
        for (int i = 0; i < array.Length; i++)
        {
            summ += array[i].Value;
        }
        return summ;
    }

    [Benchmark]
    public int ForeachArray()
    {
        var summ = 0;
        foreach (var item in array)
        {
            summ += item.Value;
        }
        return summ;
    }

    [Benchmark]
    public int ForList()
    {
        var summ = 0;
        for (int i = 0; i < list.Count; i++)
        {
            summ += list[i].Value;
        }
        return summ;
    }

    [Benchmark]
    public int ForeachList()
    {
        var summ = 0;
        foreach (var item in list)
        {
            summ += item.Value;
        }
        return summ;
    }

    [Benchmark]
    public int ForeachEnumerable()
    {
        var summ = 0;
        foreach (var item in enumerable)
        {
            summ += item.Value;
        }
        return summ;
    }

    private class TestClass
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public List<int> List { get; set; } = new List<int>();
    }
}