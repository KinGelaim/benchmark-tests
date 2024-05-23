using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.DegenerateMethods;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class DegenerateMethodsTests
{
    private DateTime? _fixedTimeWithNull = null;

    private DateTime? _fixedTimeWithoutNull = new DateTime(2023, 10, 12);


    #region First

    [Benchmark]
    public DateTime NowWithNull() => GetDateTimeNowWithNull();

    [Benchmark]
    public DateTime TodayWithNull() => GetDateTimeTodayWithNull();

    private DateTime GetDateTimeNowWithNull()
    {
        return _fixedTimeWithNull ?? DateTime.Now;
    }

    private DateTime GetDateTimeTodayWithNull()
    {
        return _fixedTimeWithNull?.Date ?? DateTime.Today;
    }

    [Benchmark]
    public DateTime NowWithoutNull() => GetDateTimeNowWithoutNull();

    [Benchmark]
    public DateTime TodayWithoutNull() => GetDateTimeTodayWithoutNull();

    private DateTime GetDateTimeNowWithoutNull()
    {
        return _fixedTimeWithoutNull ?? DateTime.Now;
    }

    private DateTime GetDateTimeTodayWithoutNull()
    {
        return _fixedTimeWithoutNull?.Date ?? DateTime.Today;
    }

    #endregion First


    #region Second

    [Benchmark]
    public DateTime NowWithNullWithoutMethod() => _fixedTimeWithNull ?? DateTime.Now;

    [Benchmark]
    public DateTime TodayWithNullWithoutMethod() => _fixedTimeWithNull?.Date ?? DateTime.Today;

    [Benchmark]
    public DateTime NowWithoutNullWithoutMethod() => _fixedTimeWithoutNull ?? DateTime.Now;

    [Benchmark]
    public DateTime TodayWithoutNullWithoutMethod() => _fixedTimeWithoutNull?.Date ?? DateTime.Today;

    #endregion Second
}