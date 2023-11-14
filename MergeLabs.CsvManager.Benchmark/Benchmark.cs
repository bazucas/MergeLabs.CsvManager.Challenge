using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace MergeLabs.CsvManager.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Benchmark
{
    private string _csvData = null!;

    [GlobalSetup]
    public void Setup()
    {
        var dataAccess = new DataAccess.CsvMockDataProvider();
        _csvData = dataAccess.RetrieveCsvData();
    }

    [Benchmark]
    public void Benchmark_SimpleTransformCsv()
    {
        Lib.CsvManager.SimpleTransformCsv(_csvData);
    }

    [Benchmark]
    public void Benchmark_TransformCsv()
    {
        Lib.CsvManager.TransformCsv(_csvData);
    }
}