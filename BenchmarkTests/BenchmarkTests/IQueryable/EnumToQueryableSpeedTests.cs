using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.IQueryable;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class EnumToQueryableSpeedTests
{
    [Params(0, 1, 6, 8, 10, 42, 100/*, 1337, 10000, 100000, 1000000*/)]
    public int Count { get; set; }

    private IEnumerable<TestClass> enumerable;
    private IQueryable<TestClass> queryable;

    [Benchmark]
    public IList<TestClass> IEnumToIQueryable() => enumerable.AsQueryable().ToList();

    [Benchmark]
    public IList<TestClass> IEnumTestClassToList() => queryable.AsEnumerable().ToList();

    [GlobalSetup]
    public void Setup()
    {
        var rnd = new Random(42);

        var array = new TestClass[Count];
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

        enumerable = new List<TestClass>(array);
        queryable = new List<TestClass>(array).AsQueryable();
    }

    public class TestClass
    {
        public int Value { get; set; }

        public string Text { get; set; } = string.Empty;

        public List<int> List { get; set; } = new List<int>();
    }
}