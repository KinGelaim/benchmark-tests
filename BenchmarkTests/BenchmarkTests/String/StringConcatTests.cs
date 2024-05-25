using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;


namespace BenchmarkTests.String;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class StringConcatTests
{
    [Params(1, 10, 100, 1000/*, 10000, 100000, 1000000*/)]
    public int N;

    [Benchmark]
    public long Plus()
    {
        var result = "";
        for (var i = 0; i < N; i++)
        {
            result += DateTime.Now.ToString();
        }

        return result.Length;
    }

    [Benchmark]
    public long Concat()
    {
        var result = "";
        for (var i = 0; i < N; i++)
        {
            result = string.Concat(result, DateTime.Now.ToString());
        }

        return result.Length;
    }

    [Benchmark]
    public long Format()
    {
        var result = "";
        for (var i = 0; i < N; i++)
        {
            result = string.Format("{0}{1}", result, DateTime.Now.ToString());
        }

        return result.Length;
    }

    [Benchmark]
    public long Interpolation()
    {
        var result = "";
        for (var i = 0; i < N; i++)
        {
            result = $"{result}{DateTime.Now}";
        }

        return result.Length;
    }

    [Benchmark]
    public long Build()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < N; i++)
        {
            builder = builder.Append(DateTime.Now.ToString());
        }

        var result = builder.ToString();
        return result.Length;
    }
}