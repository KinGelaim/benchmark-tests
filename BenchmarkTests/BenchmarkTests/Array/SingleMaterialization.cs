using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class SingleMaterialization
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

    private int searchValue = 100;

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

        searchValue = new Random().Next(0, Count);
    }

    #region Where

    [Benchmark]
    public int? FuncCountLazy()
    {
        return LazyEnum.Where(el => el == searchValue).FirstOrDefault();
    }

    [Benchmark]
    public int? FuncCountArray()
    {
        return MaterializationArray.Where(el => el == searchValue).FirstOrDefault();
    }

    [Benchmark]
    public int? FuncCountList()
    {
        return MaterializaionList.Where(el => el == searchValue).FirstOrDefault();
    }

    #endregion Where
}