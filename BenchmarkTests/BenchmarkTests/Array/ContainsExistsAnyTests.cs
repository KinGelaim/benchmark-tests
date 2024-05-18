using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class ContainsExistsAnyTests
{
    /// <summary>
    /// Максимальное значение при генерации случайных числе
    /// </summary>
    private const int MAX_VALUE = 7000000;

    /// <summary>
    /// Количнство искомых переменных
    /// </summary>
    private const int COUNT_SEARCH = 10000;

    /// <summary>
    /// Искомые переменные
    /// </summary>
    private int[] Find = new int[COUNT_SEARCH];

    private int[] array;
    private List<int> list;
    HashSet<int> hashSet;

    /// <summary>
    /// Размер выборки
    /// </summary>
    //[Params(0, 5, 10, 100, 1000, 10000, 100000, 1000000)]
    [Params(0, 5, 10, 100, 1000, 10000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        list = new List<int>(Count);

        // Заполняем коллекции
        var random = new Random();
        for (int i = 0; i < Count; i++)
        {
            list.Add(random.Next(MAX_VALUE));
        }
        array = list.ToArray();
        hashSet = new HashSet<int>(list);

        // Создаём значения, которые будем искать
        random = new Random();
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            Find[i] = random.Next(MAX_VALUE);
        }
    }


    #region Array

    [Benchmark]
    public void ContainsArray()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            array.Contains(Find[i]);
        }
    }

    [Benchmark]
    public void ExistsArray()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            Array.Exists(array, a => a == Find[i]);
        }
    }

    [Benchmark]
    public void AnyArray()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            array.Any(s => s == Find[i]);
        }
    }

    #endregion Array


    #region List

    [Benchmark]
    public void ContainsList()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            list.Contains(Find[i]);
        }
    }

    [Benchmark]
    public void ExistsList()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            list.Exists(s => s == Find[i]);
        }
    }

    [Benchmark]
    public void AnyList()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            list.Any(s => s == Find[i]);
        }
    }

    #endregion List


    #region HashSet

    [Benchmark]
    public void ContainsHashSet()
    {
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            hashSet.Contains(Find[i]);
        }
    }

    #endregion HashSet
}