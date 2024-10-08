﻿﻿# MergeLabs.CsvManager.Challenge

# Executing the Application and Running Unit Tests

## Running the Application

To run the application, follow these steps:

1. **Clone the Repository**: First, clone the repository to your local machine.

2. **Switch to Release Mode**: Ensure the application is set to run in release mode for optimal performance.

3. **Select the Benchmark Project**: Choose the benchmark project as the startup project in your development environment.

4. **View Results**: Run the application, and observe the results in the console app.

## Running Unit Tests

To execute the unit tests:

- Simply run all the tests available in the test project. This can usually be done through the test explorer in your development environment.

## Assumptions and Approach

In developing the SimpleTransformCsv method, I utilized LINQ and extension methods, aligning with my personal approach for solving such requirements. Conversely, the TransformCsv method was crafted using a more straightforward strategy, employing for loops and StringBuilder to achieve similar outcomes.

To facilitate benchmarking, I set up a dedicated project, leveraging a local CSV file filled with various mock data. This data could alternatively originate from other sources like services or databases.

In this implementation, I adhered strictly to the specified requirements. As such, elements like exception handling, logging, or additional features were not incorporated into the codebase. This decision was also influenced by my practice of using stable software versions; hence, despite the recent release of .Net8, I chose to work with the tried and tested .Net7 version, avoiding beta, preview, and rc versions of frameworks.

## Benchmark results

Both methods achieve the intended task, but the observable results through BenchmarkDotNet affirm that despite having very similar outcomes, LINQ and extension methods are becoming increasingly performant and reliable, capable of carrying out these tasks effectively and efficiently.

![Benchmark Results](./images/test.png)

Two more methods from a colleague have been added to the project, to carry out the unit tests and the benchmark, and here are the results.

![Benchmark Results](./images/test2.png)

![Benchmark Results](./images/test3.png)

![Benchmark Results](./images/test4.png)


# Technical Interview Question #

This interview questionnaire consists of three components and has been structured using tools tailored to the requirements of the position we aim to fulfill: Two CSV data transformation methods and performance benchmarking. Demonstrate your coding, testing, and benchmarking skills, using Visual Studio 2022 only.

Part One: Implement and test `SimpleTransformCsv` method in the `CsvManager` class. This method should remove "NULL" rows, retaining the header.

Part Two: Extend `CsvManager` to include a second method, `TransformCsv`, that performs the same task as the `SimpleTransformCsv` method, but with a new distinct approach. Implement the provided unit test.

Part Three: Benchmark `SimpleTransformCsv` and `TransformCsv` with your chosen tool. Provide insights to the benchmark(s).

### Submission Instructions: ###

Ideally, for us please create a GitHub repository for your solution and upload the code, including the CsvManager class, unit tests, and performance benchmarks. Being able to showcase well-organized Git pushes is a good thing. Provide the URL via email, of the GitHub repository for us to clone and run.

Alternatively, compress your solution into a ZIP file and share it via a shared link, not attached to the email please.

You will be provided an email to submit details to in an earlier correspondence.

### Overall Evaluation Criteria: ###

As part of the evaluation process, we expect to be able to clone and execute the solution, without requiring any manual intervention, it should compile and run in Visual Studio 2022 and so should have included within it working solution and projects files. Please make the assumption that we have access to the public NuGet repository.

We also take a look at the contents of the solution and project files during our evaluation process.

We appreciate it if you can incorporate the latest versions of .NET and C# and any good coding practices where they make sense. We understand that the scope of these questions may not allow you to showcase your full capabilities, but hope there will be slightly more of an opportunity to do so in the second part of the exam when working on the `TransformCsv` method.

## Title: CSV Data Transformation (Part 1 of 3) ##

### Instructions: ###
In this technical question, you will be asked to revise and complete the `CsvManager` class to implement a CSV data transformation feature. You are provided with a partially implemented `CsvManager` class, and your task is to complete the `SimpleTransformCsv` method based on the specified requirements.

### CSV Transformation Requirements: ###

Your objective is to implement a method that performs a transformation on CSV data while adhering to the following rules:

1. Implement the `SimpleTransformCsv` method, which should accept a CSV string as input and return a new CSV string as output.

2. The input CSV string may consist of one or more rows, with each row separated by the newline character ('\n') and cells within a row separated by a comma (',').

3. The transformation should exclude any rows where any cell contains the value "NULL."

4. The header row (first row) should be preserved in the output without changes.

#### Your Task: ####

Complete the `SimpleTransformCsv` method within the `CsvManager` class to meet the specified requirements. 

```csharp
public class CsvManager
{
    private const char CsvNewLineCharacter = '\n';
    private const char CsvCellSeperator = ',';
    private const string CsvNullCellIdentifier = "NULL";

    public static string SimpleTransformCsv(string S)
    {
        // Your solution here.
    }
}
```

#### Example: ####

As an example, if the input CSV string is:

```csv
id,name,age,score
1,Jack,NULL,12
17,Betty,28,11
```

The transformed CSV string should be:

```csv
id,name,age,score
17,Betty,28,11
```

### Unit Testing: ###

Here is the unit test code that should be ran against it:

```csharp
[TestClass]
public class UnitTest
{
    public required TestContext TestContext { get; init; }

    [TestMethod]
    [DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
    public void TestSimpleTransformCsvMethod(string display_name, string input, string expected)
    {
        TestContext.WriteLine($" > {display_name}");
        string results = CsvManager.SimpleTransformCsv(input);
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
```

### Evaluation Criteria: ###

Your performance will be assessed based on the correctness and efficiency of your solution.

While comments placed between lines of code are not required, it is optional if you wish to include XML documentation comments on the methods themselves to enhance the clarity of your code and provide insights into the purpose and usage of the methods.

# Technical Interview Question - Part Two #

## Title: CSV Data Transformation (Part 2 of 3) ##

### Instructions: ###

In this second part of the technical interview question, you will continue working with the `CsvManager` class to implement a second CSV data transformation method, `TransformCsv`. This method should achieve the same transformation as the previously implemented `SimpleTransformCsv` method but employing an alternative methodology in any manner of your discretion. Feel unrestrained to exercise your creativity and employ coding techniques that align more closely with your personal coding preferences, should such inclinations be your disposition. Your task is to add the `TransformCsv` method to the `CsvManager` class and use the provided unit test to validate the method.

### CSV Transformation Requirements: ###

You should implement the `TransformCsv` method with the following requirements:

1. Implement the `TransformCsv` method, which should accept a CSV string as input and return a new CSV string as output.

2. The input CSV string may consist of one or more rows, with each row separated by the newline character ('\n') and cells within a row separated by a comma (',').

3. The transformation should exclude any rows where any cell contains the value "NULL."

4. The header row (first row) should be preserved in the output without changes.

### Your Task: ###

Extend the `CsvManager` class by adding the `TransformCsv` method to meet the specified requirements. This method should provide a different approach to achieve the same transformation as the `SimpleTransformCsv` method.

```csharp
public class CsvManager
{
    private const char CsvNewLineCharacter = '\n';
    private const char CsvCellSeperator = ',';
    private const string CsvNullCellIdentifier = "NULL";

    public static string SimpleTransformCsv(string S)
    {
        // Your original solution here.
    }

    public static string TransformCsv(string S)
    {
        // Your new approach here.
    }
}
```

### Unit Testing: ###

You should use the provided unit  to validate the method (`TransformCsv`) and that the two tests `TestTransformCsvMethod` and `TestSimpleTransformCsvMethod` are tested independently and its behavior is verified without interference from the other method.

Unit test for `CsvManager.TestTransformCsvMethod`:

```csharp
[TestMethod]
[DynamicData(nameof(TestDataSet.GenerateBasicDataSet), typeof(TestDataSet), DynamicDataSourceType.Method)]
public void TestTransformCsvMethod(string display_name, string input, string expected)
{
    TestContext.WriteLine($" > {display_name}");
    string results = CsvManager.TransformCsv(input);
    Assert.AreEqual(expected, results);
}
```

## Evaluation Criteria: ##

Your performance will be assessed based code readability and on your ability to provide an alternative solution in the `TransformCsv` method in comparison to the `SimpleTransformCsv` method.

While comments placed between lines of code are not required, it is optional if you wish to include XML documentation comments on the methods themselves to enhance the clarity of your code and provide insights into the purpose and usage of the methods.

# Technical Interview Question - Part Three/Final #

## Title: CSV Data Performance Benchmark (Part 3 of 3) ##

### Instructions: ###

In this final part of the technical interview question, your focus is on benchmarking the performance of the two CSV data transformation methods, `SimpleTransformCsv` and `TransformCsv`, created in Parts One and Two. Your task is to create benchmarks using a benchmarking tool of your choice to compare the execution times of these methods and evaluate their performance.

### Performance Benchmark Requirements: ###

1. Select a benchmarking tool or library of your preference, such as [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet), to conduct performance benchmarks for the `SimpleTransformCsv` and `TransformCsv` methods.

2. Develop benchmark tests that provide input data representative for these methods.

3. Measure and compare the execution times of both methods. You may use the data already defined in the `TestDataSet.GenerateBasicDataSet()` method if it suits your testing scenario.

4. Integrate the benchmark results into the project/solution so that they are easily accessible. Optionally, consider providing a `RESULTS.md` markdown file, if you feel that the generated output from the benchmarks require futher explaination, but ensure it can be reviewed within 4-5 minutes or less. Conciseness is valued in this case.

### Your Task: ###

Create performance benchmarks for the `SimpleTransformCsv` and `TransformCsv` methods using your chosen benchmarking tool. Include the source code for the benchmarks and any output generated by it.

### Evaluation Criteria: ###

Your performance in this part of the interview will be assessed based on your ability to present your benchmarks clearly and comprehensibly.

---

### Bonus ###

If you are able to include the following feature into your project files, that would be great:
```xml
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```
