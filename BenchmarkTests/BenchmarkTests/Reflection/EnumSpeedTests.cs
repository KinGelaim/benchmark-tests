using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkTests.Reflection.Enums;


namespace BenchmarkTests.Reflection;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class EnumSpeedTests
{
    private Array CacheSmallEnum;
    private IEnumerable<SmallEnum> CacheSmallEnumWithCast;

    [GlobalSetup]
    public void Setup()
    {
        CacheSmallEnum = Enum.GetValues(typeof(SmallEnum));
        CacheSmallEnumWithCast = Enum.GetValues(typeof(SmallEnum)).Cast<SmallEnum>();
    }

    [Benchmark]
    public int GetValuesFromSmallEnum()
    {
        var enums = Enum.GetValues(typeof(SmallEnum));
        return enums.Length;
    }

    [Benchmark]
    public int GetValuesFromSmallEnumWithCast()
    {
        var enums = Enum.GetValues(typeof(SmallEnum)).Cast<SmallEnum>();
        return enums.Count();
    }

    [Benchmark]
    public int GetValuesFromSmallEnumWithCache()
    {
        return CacheSmallEnum.Length;
    }

    [Benchmark]
    public int GetValuesFromSmallEnumWithCastWithCache()
    {
        return CacheSmallEnumWithCast.Count();
    }
}