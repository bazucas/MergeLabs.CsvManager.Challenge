namespace MergeLabs.CsvManager.Tests;
using CsvManager = Lib.CsvManager;

[TestClass]
public class CsvManagerTests
{
    public required TestContext TestContext { get; init; }

    [TestMethod]
    [DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
    public void TestSimpleTransformCsvMethod(string display_name, string input, string expected)
    {
        TestContext.WriteLine($" > {display_name}");
        var results = CsvManager.SimpleTransformCsv(input);
        Assert.AreEqual(expected, results);
    }

    [TestMethod]
    [DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
    public void TestTransformCsvMethod(string display_name, string input, string expected)
    {
        TestContext.WriteLine($" > {display_name}");
        var results = CsvManager.TransformCsv(input);
        Assert.AreEqual(expected, results);
    }

    [TestMethod]
    [DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
    public void TestSimpleTransformCsvMethod_NotMine(string display_name, string input, string expected)
    {
        TestContext.WriteLine($" > {display_name}");
        var results = CsvManager.SimpleTransformCsv_NotMine(input);
        Assert.AreEqual(expected, results);
    }

    [TestMethod]
    [DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
    public void TestTransformCsvMethod_NotMine(string display_name, string input, string expected)
    {
        TestContext.WriteLine($" > {display_name}");
        var results = CsvManager.TransformCsv_NotMine(input);
        Assert.AreEqual(expected, results);
    }
}

public static class TestDataSet
{
    public static IEnumerable<object[]> GenerateBasicDataSet()
    {
        List<object[]> data = new()
        {
            new object[] { "Example.1", "id,name,age,score\n1,Jack,NULL,12\n17,Betty,28,11", "id,name,age,score\n17,Betty,28,11" },
            new object[] { "Example.2", "header,header\nANNUL,ANNULLED\nnull,NILL\nNULL,NULL", "header,header\nANNUL,ANNULLED\nnull,NILL" },
            new object[] { "Example.3", "country,population,area\nUK,67m,242000km2", "country,population,area\nUK,67m,242000km2" }
        };
        return data;
    }
}