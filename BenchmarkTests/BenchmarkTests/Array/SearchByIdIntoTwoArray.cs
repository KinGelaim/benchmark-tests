using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class SearchByIdIntoTwoArray
{
    /// <summary>
    /// Максимальное значение при генерации случайных числе
    /// </summary>
    private const int MAX_VALUE = 7000000;

    /// <summary>
    /// Количество искомых переменных
    /// </summary>
    private const int COUNT_SEARCH = 10000;

    /// <summary>
    /// Искомые переменные
    /// </summary>
    private int[] Find = new int[COUNT_SEARCH];

    private int[] array1;
    private int[] array2;
    private List<int> list1;
    private List<int> list2;

    /// <summary>
    /// Размер выборки
    /// </summary>
    //[Params(0, 5, 10, 100, 1000, 10000, 100000, 1000000)]
    [Params(0, 5, 10, 100, 1000, 10000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        list1 = new List<int>(Count);
        list2 = new List<int>(Count);

        // Заполняем коллекции
        var random = new Random();
        for (int i = 0; i < Count; i++)
        {
            list1.Add(random.Next(MAX_VALUE));
            list2.Add(random.Next(MAX_VALUE));
        }
        array1 = list1.ToArray();
        array2 = list2.ToArray();

        // Создаём значения, которые будем искать
        random = new Random();
        for (int i = 0; i < COUNT_SEARCH; i++)
        {
            Find[i] = random.Next(MAX_VALUE);
        }
    }


    #region Array

    [Benchmark]
    public int ForInForArray()
    {
        var sum = 0;
        for (int i = 0; i < array1.Length; i++)
        {
            for (int j = 0; j < array2.Length; j++)
            {
                if (array1[i] == array2[j])
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForInForBreakArray()
    {
        var sum = 0;
        for (int i = 0; i < array1.Length; i++)
        {
            for (int j = 0; j < array2.Length; j++)
            {
                if (array1[i] == array2[j])
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachInForeachArray()
    {
        var sum = 0;
        foreach (var item1 in array1)
        {
            foreach (var item2 in array2)
            {
                if (item1 == item2)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachInForeachBreakArray()
    {
        var sum = 0;
        foreach (var item1 in array1)
        {
            foreach (var item2 in array2)
            {
                if (item1 == item2)
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachWithListContainsArray()
    {
        var IDs = array2.Select(x => x).ToList();
        var sum = 0;
        foreach (var item1 in array1)
        {
            if (IDs.Contains(item1))
            {
                sum++;
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachWithHashSetContainsArray()
    {
        var IDs = array2.Select(x => x).ToHashSet();
        var sum = 0;
        foreach (var item1 in array1)
        {
            if (IDs.Contains(item1))
            {
                sum++;
            }
        }
        return sum;
    }

    [Benchmark]
    public int CreateNewListWithListContainsArray()
    {
        var IDs = array2.Select(x => x).ToList();
        var search = array1.Where(item => IDs.Contains(item)).ToArray();
        var sum = 0;
        foreach (var item in search)
        {
            sum++;
        }
        return sum;
    }

    [Benchmark]
    public int CreateNewListWithHashSetContainsArray()
    {
        var IDs = array2.Select(x => x).ToHashSet();
        var search = array1.Where(item => IDs.Contains(item)).ToArray();
        var sum = 0;
        foreach (var item in search)
        {
            sum++;
        }
        return sum;
    }

    #endregion Array


    #region List

    [Benchmark]
    public int ForInForList()
    {
        var sum = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            for (int j = 0; j < list2.Count; j++)
            {
                if (array1[i] == array2[j])
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForInForBreakList()
    {
        var sum = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            for (int j = 0; j < list2.Count; j++)
            {
                if (array1[i] == array2[j])
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachInForeachList()
    {
        var sum = 0;
        foreach (var item1 in list1)
        {
            foreach (var item2 in list2)
            {
                if (item1 == item2)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachInForeachBreakList()
    {
        var sum = 0;
        foreach (var item1 in list1)
        {
            foreach (var item2 in list2)
            {
                if (item1 == item2)
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachWithListContainsList()
    {
        var IDs = list2.Select(x => x).ToList();
        var sum = 0;
        foreach (var item1 in list1)
        {
            if (IDs.Contains(item1))
            {
                sum++;
            }
        }
        return sum;
    }

    [Benchmark]
    public int ForeachWithHashSetContainsList()
    {
        var IDs = list2.Select(x => x).ToHashSet();
        var sum = 0;
        foreach (var item1 in list1)
        {
            if (IDs.Contains(item1))
            {
                sum++;
            }
        }
        return sum;
    }

    [Benchmark]
    public int CreateNewListWithListContainsList()
    {
        var IDs = list2.Select(x => x).ToList();
        var search = list1.Where(item => IDs.Contains(item)).ToList();
        var sum = 0;
        foreach (var item in search)
        {
            sum++;
        }
        return sum;
    }

    [Benchmark]
    public int CreateNewListWithHashSetContainsList()
    {
        var IDs = list2.Select(x => x).ToHashSet();
        var search = list1.Where(item => IDs.Contains(item)).ToList();
        var sum = 0;
        foreach (var item in search)
        {
            sum++;
        }
        return sum;
    }

    #endregion List
}