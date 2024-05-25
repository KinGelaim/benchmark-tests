using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace BenchmarkTests.Reflection;

[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class GetSetNameSpeedTests
{
    private static readonly Test _t = new Test();

    private static readonly PropertyInfo _cache = _t
        .GetType()
        .GetProperty("Login")!;

    [Benchmark(Description = "GetName")]
    public string? GetName()
    {
        return _t
            .GetType()
            .GetProperty("Login")?
            .GetValue(_t)?
            .ToString();
    }

    [Benchmark]
    public void SetName()
    {
        _t
            .GetType()
            .GetProperty("Login")?
            .SetValue(_t, "MAKotkov");
    }

    [Benchmark]
    public void GetNameCache() => _cache.GetValue(_t)!.ToString();

    [Benchmark]
    public void SetNameCache() => _cache.SetValue(_t, "MAKotkov");
}