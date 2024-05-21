using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkTests.Arrays;
using BenchmarkTests.DateTimeTests;
using System.Diagnostics;


// ---DEBUG---
//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());


#region Массивы

// Скорость прохода по массивам For, Foreach in Array, List, IEnumerable проверяется на типе данных Byte
BenchmarkRunner.Run<ArraySpeedTests>();

// Скорость прохода по массивам For, Foreach in Array, List, IEnumerable проверяется на типе данных class TestClass
BenchmarkRunner.Run<ArrayClassSpeedTests>();

// Скорость ToArray против ToList
BenchmarkRunner.Run<ToArrayVsToListSpeedTests>();

// Скорость методов Contains, Exists, Any для Array, List, HashSet
BenchmarkRunner.Run<ContainsExistsAnyTests>();

// Скорость поиска пересечения в двух выборках (массив и коллекция)
BenchmarkRunner.Run<SearchByIdIntoTwoArray>();

// Скорость вызова методов Count, Any, Проход по массиву с суммирование на ленивом фильтре, материализованном в массив, материализованном в лист
BenchmarkRunner.Run<EnumMaterialization>();
BenchmarkRunner.Run<SingleMaterialization>();

// Скорость нахождения длины
BenchmarkRunner.Run<AnyVsLength>();

#endregion Массивы


#region Даты

BenchmarkRunner.Run<DateTimeMonthNameSpeed>();

#endregion Даты