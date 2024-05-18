using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.Arrays;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class AnyVsLength
{
    private readonly string _str = "hello";
    private readonly List<int> _list = new() { 1, 2, 3 };
    private readonly int[] _array = new int[] { 4, 5, 6 };
    //private IEnumerable<byte> _enum = Enumerable.Empty<byte>();

    //[GlobalSetup]
    //public void Setup()
    //{
    //    var array = new byte[100000];
    //    new Random(42).NextBytes(array);

    //    _enum = array;
    //}

    [Benchmark(Baseline = true)]
    public bool AllNonEmpty_Any_All() =>
        _str.Any() &&
        _list.Any() &&
        _array.Any();

    [Benchmark]
    public bool AllNonEmpty_Property_All() =>
        _str.Length != 0 &&
        _list.Count != 0 &&
        _array.Length != 0;

    [Benchmark]
    public bool NonEmpty_Any_Str() => _str.Any();

    [Benchmark]
    public bool NonEmpty_Any_List() => _list.Any();

    [Benchmark]
    public bool NonEmpty_Any_Array() => _array.Any();

    //[Benchmark]
    //public bool NonEmpty_Any_Enumerable() => _enum.Any();

    [Benchmark]
    public bool NonEmpty_Property_Str() => _str.Length != 0;

    [Benchmark]
    public bool NonEmpty_Property_List() => _list.Count != 0;

    [Benchmark]
    public bool NonEmpty_Property_Array() => _array.Length != 0;

    //[Benchmark]
    //public bool NonEmpty_Method_Enumerable() => _enum.Count() != 0;
}