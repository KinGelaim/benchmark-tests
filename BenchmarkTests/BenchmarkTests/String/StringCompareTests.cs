using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.String;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class StringCompareTests
{
    //[Params(1, 10, 100, 1000/*, 10000, 100000, 1000000*/)]
    //public int N;

    private string _compare1 = "xxxxxxxxxxxxxxxxxx";
    private string _compare2 = "xxxxxxxxxxxxxxxxxx";
    private char[] _scheme2 = new char[0];
    private byte[] _scheme2Byte = new byte[0];

    private string _compare3 = "ueoqwwnsdlkskjsowy";
    private string _compare4 = "sakjdjsjahsdhsjdak";

    private string _compare5 = "xxxxxxxxxxxxxxsowy";
    private string _compare6 = "xxxxxxxxxxxxxxjdak";

    [GlobalSetup]
    public void Setup()
    {
        _scheme2 = _compare2.ToArray();
        //_scheme2Byte = "qwe"u8.ToArray();
    }

    #region SET 1 compared strings are the same

    [Benchmark]
    public bool BasicCompare()
    {
        return _compare1 == _compare2;
    }

    [Benchmark]
    public bool EqualsCompare()
    {
        return _compare1.Equals(_compare2);
    }

    [Benchmark]
    public bool EqualsStringComparisonCompare()
    {
        return _compare1.Equals(_compare2, StringComparison.Ordinal);
    }

    [Benchmark]
    public bool StringCompare()
    {
        return string.Compare(_compare1, _compare2, StringComparison.Ordinal) != 0;
    }

    [Benchmark]
    public bool StringCompareOrdinal()
    {
        return string.CompareOrdinal(_compare1, _compare2) != 0;
    }

    [Benchmark]
    public bool SequenceEqual()
    {
        var _scheme = _compare1.ToArray();
        return _compare2.SequenceEqual(_scheme);
    }

    [Benchmark]
    public bool SequenceEqualWithSetup()
    {
        return _compare2.SequenceEqual(_scheme2);
    }

    #endregion SET 1 compared strings are the same


    #region SET 2 compared strings are NOT the same

    [Benchmark]
    public bool BasicCompareNotSame()
    {
        return _compare3 == _compare4;
    }

    [Benchmark]
    public bool EqualsCompareNotSame()
    {
        return _compare3.Equals(_compare4);
    }

    [Benchmark]
    public bool EqualsStringComparisonCompareNotSame()
    {
        return _compare3.Equals(_compare4, StringComparison.Ordinal);
    }

    [Benchmark]
    public bool StringCompareNotSame()
    {
        return string.Compare(_compare3, _compare4, StringComparison.Ordinal) != 0;
    }

    [Benchmark]
    public bool StringCompareOrdinalNotSame()
    {
        return string.CompareOrdinal(_compare3, _compare4) != 0;
    }

    [Benchmark]
    public bool SequenceEqualNotSame()
    {
        var _scheme = _compare3.ToArray();
        return _compare4.SequenceEqual(_scheme);
    }

    #endregion SET 2 compared strings are NOT the same


    #region SET 3 the compared strings partially same

    [Benchmark]
    public bool BasicComparePartiallySame()
    {
        return _compare5 == _compare6;
    }

    [Benchmark]
    public bool EqualsComparePartiallySame()
    {
        return _compare5.Equals(_compare6);
    }

    [Benchmark]
    public bool EqualsStringComparisonComparePartiallySame()
    {
        return _compare5.Equals(_compare6, StringComparison.Ordinal);
    }

    [Benchmark]
    public bool StringComparePartiallySame()
    {
        return string.Compare(_compare5, _compare6, StringComparison.Ordinal) != 0;
    }

    [Benchmark]
    public bool StringCompareOrdinalPartiallySame()
    {
        return string.CompareOrdinal(_compare5, _compare6) != 0;
    }

    [Benchmark]
    public bool SequenceEqualPartiallySame()
    {
        var _scheme = _compare5.ToArray();
        return _compare6.SequenceEqual(_scheme);
    }

    #endregion SET 3 the compared strings partially same
}