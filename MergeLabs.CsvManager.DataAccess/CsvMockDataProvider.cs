namespace MergeLabs.CsvManager.DataAccess;

public class CsvMockDataProvider
{
    // could retrieve this from local or azure appSettings or through key vault as well
    private const string CsvFilePath = "./CsvFile/mockdata.csv";

    /// <summary>
    /// Reads and returns the content of a CSV file as a single string.
    /// </summary>
    /// <remarks>
    /// This method checks if the specified CSV file exists at the defined path.
    /// If the file does not exist, it returns an empty string. Otherwise, it reads the entire content
    /// of the file and returns it as a string, preserving the original line breaks of the file.
    /// </remarks>
    /// <returns>
    /// A string containing the entire content of the CSV file if the file exists, or an empty string if the file does not exist.
    /// </returns>
    public string RetrieveCsvData() =>
        !File.Exists(CsvFilePath) ? string.Empty : File.ReadAllText(CsvFilePath);
}