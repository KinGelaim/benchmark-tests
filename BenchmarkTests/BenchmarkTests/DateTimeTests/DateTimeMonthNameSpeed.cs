using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace BenchmarkTests.DateTimeTests;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[RPlotExporter, RankColumn]
public class DateTimeMonthNameSpeed
{
    [Params(0, 1, 6, 8, 10, 42, 100, 10000, 100000, 1000000)]
    public int Count { get; set; }

    private IEnumerable<DateTime> _dates;

    [GlobalSetup]
    public void Setup()
    {
        var rnd = new Random(42);

        var array = new DateTime[Count];
        for (int i = 0; i < Count; i++)
        {
            var rndValue = rnd.Next(-1000, 1000);

            var testDateTime = DateTime.Now;
            testDateTime.AddDays(rndValue);

            array[i] = testDateTime;
        }

        _dates = new List<DateTime>(array);
    }

    [Benchmark]
    public IList<string> GetMonthNameWithConvertChart()
    {
        var result = new List<string>(_dates.Count());

        foreach (var date in _dates)
        {
            result.Add(GetMonthOrDefault(_monthsInfo, date.Month)?.Name ?? "");
        }

        return result;
    }

    [Benchmark]
    public IList<string> GetMonthNameWithArray()
    {
        var result = new List<string>(_dates.Count());

        foreach (var date in _dates)
        {
            result.Add(_monthsArray[date.Month - 1] ?? "");
        }

        return result;
    }

    private static readonly string[] _monthsArray = new[]
    {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        };

    [Benchmark]
    public IList<string> GetMonthNameWithDictionary()
    {
        var result = new List<string>(_dates.Count());

        foreach (var date in _dates)
        {
            result.Add(_monthsDictionary.GetValueOrDefault(date.Month) ?? "");
        }

        return result;
    }

    private static readonly Dictionary<int, string> _monthsDictionary = new Dictionary<int, string>
        {
            { 1, "Январь" },
            { 2, "Февраль" },
            { 3, "Март" },
            { 4, "Апрель" },
            { 5, "Май" },
            { 6, "Июнь" },
            { 7, "Июль" },
            { 8, "Август" },
            { 9, "Сентябрь" },
            { 10, "Октябрь" },
            { 11, "Ноябрь" },
            { 12, "Декабрь" }
        };

    [Benchmark]
    public IList<string> GetMonthNameWithMonthObject()
    {
        var result = new List<string>(_dates.Count());

        foreach (var date in _dates)
        {
            var monthNameArray = date.ToString("MMMM").ToCharArray();

            monthNameArray[0] = char.ToUpper(monthNameArray[0]);

            result.Add(new string(monthNameArray));
        }

        return result;
    }

    private static readonly IEnumerable<Month> _monthsInfo =
    [
        new Month(1, "Январь", "январе"),
        new Month(2, "Февраль", "феврале"),
        new Month(3, "Март", "марте"),
        new Month(4, "Апрель", "апреле"),
        new Month(5, "Май", "мае"),
        new Month(6, "Июнь", "июне"),
        new Month(7, "Июль", "июле"),
        new Month(8, "Август", "августе"),
        new Month(9, "Сентябрь", "сентябре"),
        new Month(10, "Октябрь", "октябре"),
        new Month(11, "Ноябрь", "ноябре"),
        new Month(12, "Декабрь", "декабре")
    ];

    private sealed class Month
    {
        /// <summary>
        /// Номер месяца
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// Наименование месяца в формате "Январь"
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Наименование месяца в предложном падеже "январе"
        /// </summary>
        public string PrepositionalCaseName { get; init; } = string.Empty;

        public Month(int number, string name, string prepositionalCaseName)
        {
            Number = number;
            Name = name;
            PrepositionalCaseName = prepositionalCaseName;
        }
    }

    private Month? GetMonthOrDefault(IEnumerable<Month> monthsInfo, int monthNumber)
        => monthsInfo.FirstOrDefault(month => month.Number == monthNumber);
}
