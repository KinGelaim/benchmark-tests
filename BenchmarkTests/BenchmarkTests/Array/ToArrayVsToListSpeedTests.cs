using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class ToArrayVsToListSpeedTests
{
    [Params(0, 1, 6, 8, 10, 42, 100, 1337, 10000, 100000, 1000000)]
    public int Count { get; set; }


    #region First

    public IEnumerable<int> IEnumItems => Enumerable.Range(0, Count).Where(i => i > 0);

    [Benchmark(Baseline = true)]
    public int[] IEnumToArray() => IEnumItems.ToArray();

    [Benchmark]
    public List<int> IEnumToList() => IEnumItems.ToList();

    #endregion First


    #region Second

    private TestClass[] array;
    private List<TestClass> list;
    private IEnumerable<TestClass> enumerable;

    [Benchmark]
    public TestClass[] ArrayTestClassToArray() => array.ToArray();

    [Benchmark]
    public TestClass[] ListTestClassToArray() => list.ToArray();

    [Benchmark]
    public TestClass[] IEnumTestClassToArray() => enumerable.ToArray();

    [Benchmark]
    public List<TestClass> ArrayTestClassToList() => array.ToList();

    [Benchmark]
    public List<TestClass> ListTestClassToList() => list.ToList();

    [Benchmark]
    public List<TestClass> IEnumTestClassToList() => enumerable.ToList();

    [GlobalSetup]
    public void Setup()
    {
        var rnd = new Random(42);

        array = new TestClass[Count];
        for (int i = 0; i < Count; i++)
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

    public class TestClass
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public List<int> List { get; set; } = new List<int>();
    }

    #endregion Second
}