using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.ExceptionTests;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class NoExceptionTests
{
    public Random rnd;

    [GlobalSetup]
    public void Setup()
    {
        rnd = new Random(69);
    }

    [Benchmark]
    public bool DividedByZeroWithError()
    {
        var firstValue = rnd.Next(-10, 10);
        var secondValue = rnd.Next(-10, 10);

        return DividedByZeroWithError(firstValue, secondValue);
    }

    [Benchmark]
    public bool DividedByZeroWithDivideByZeroError()
    {
        var firstValue = rnd.Next(-10, 10);
        var secondValue = rnd.Next(-10, 10);

        return DividedByZeroWithDivideByZeroError(firstValue, secondValue);
    }

    [Benchmark]
    public bool DividedByZeroWithoutError()
    {
        var firstValue = rnd.Next(-10, 10);
        var secondValue = rnd.Next(-10, 10);

        return DividedByZeroWithoutError(firstValue, secondValue);
    }

    [Benchmark]
    public bool DividedByZeroBeforeCheck()
    {
        var firstValue = rnd.Next(-10, 10);
        var secondValue = rnd.Next(-10, 10);

        if (secondValue == 0)
        {
            return false;
        }

        return DividedByZeroBeforeCheck(firstValue, secondValue);
    }

    private bool DividedByZeroWithError(int firstValue, int secondValue)
    {
        try
        {
            var firstResult = firstValue / secondValue;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool DividedByZeroWithDivideByZeroError(int firstValue, int secondValue)
    {
        try
        {
            var firstResult = firstValue / secondValue;
            return true;
        }
        catch (DivideByZeroException ex)
        {
            return false;
        }
    }

    private bool DividedByZeroWithoutError(int firstValue, int secondValue)
    {
        if (secondValue == 0)
        {
            return false;
        }

        var firstResult = firstValue / secondValue;
        return true;
    }

    private bool DividedByZeroBeforeCheck(int firstValue, int secondValue)
    {
        var firstResult = firstValue / secondValue;
        return true;
    }
}