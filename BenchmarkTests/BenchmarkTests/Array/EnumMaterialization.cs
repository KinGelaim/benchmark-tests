using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class EnumMaterialization
{
    /// <summary>
    /// Размер выборки
    /// </summary>
    //[Params(0, 5, 10, 100, 1000, 10000, 100000, 1000000)]
    [Params(0, 5, 10, 100, 1000, 10000)]
    public int Count { get; set; }

    /// <summary>
    /// Генерация выборки
    /// </summary>
    public IEnumerable<int> IEnumItems => Enumerable.Range(0, Count);

    public IEnumerable<int> LazyEnum;

    public IEnumerable<int> MaterializationArray;

    public IEnumerable<int> MaterializaionList;

    [GlobalSetup]
    public void Setup()
    {
        LazyEnum = IEnumItems
            .Where(n => n % 149 == 7)
            .Where(n => n % 271 == 9);

        MaterializationArray = IEnumItems
            .Where(n => n % 149 == 7)
            .Where(n => n % 271 == 9)
            .ToArray();

        MaterializaionList = IEnumItems
            .Where(n => n % 149 == 7)
            .Where(n => n % 271 == 9)
            .ToList();
    }

    #region Count()

    [Benchmark]
    public int FuncCountLazy()
    {
        return LazyEnum.Count();
    }

    [Benchmark]
    public int FuncCountArray()
    {
        return MaterializationArray.Count();
    }

    [Benchmark]
    public int FuncCountList()
    {
        return MaterializaionList.Count();
    }

    #endregion Count()


    #region Any

    [Benchmark]
    public bool AnyLazy()
    {
        return LazyEnum.Any();
    }

    [Benchmark]
    public bool AnyArray()
    {
        return MaterializationArray.Any();
    }

    [Benchmark]
    public bool AnyList()
    {
        return MaterializaionList.Any();
    }

    #endregion Any


    #region Sum

    [Benchmark]
    public int SumLazy()
    {
        var sum = 0;
        foreach (var item in LazyEnum)
        {
            sum += item;
        }
        return sum;
    }

    [Benchmark]
    public int SumArray()
    {
        var sum = 0;
        foreach (var item in MaterializationArray)
        {
            sum += item;
        }
        return sum;
    }

    [Benchmark]
    public int SumList()
    {
        var sum = 0;
        foreach (var item in MaterializaionList)
        {
            sum += item;
        }
        return sum;
    }

    #endregion Sum
}