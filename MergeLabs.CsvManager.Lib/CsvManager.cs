using System.Text;

namespace MergeLabs.CsvManager.Lib;

public class CsvManager
{
    private const char CsvNewLineCharacter = '\n';
    private const char CsvCellSeparator = ',';
    private const string CsvNullCellIdentifier = "NULL";

    /// <summary>
    /// Transforms a CSV string by removing lines containing "NULL" in any cell.
    /// </summary>
    /// <param name="S">The CSV string to be transformed.</param>
    /// <remarks>
    /// This method processes the input CSV string line by line.
    /// Each line is split into cells, and any line containing a cell exactly equal to "NULL" is excluded.
    /// The transformation preserves the integrity of other lines and their original formatting.
    /// </remarks>
    /// <returns>
    /// A transformed CSV string with lines containing "NULL" removed.
    /// </returns>
    public static string SimpleTransformCsv(string S)
    {
        // check if the input string is null or empty, if it is, returns an empty string
        if (string.IsNullOrEmpty(S))
        {
            return string.Empty;
        }

        // split the input string into lines using the \n character, this creates an array of strings,
        // each representing a line in the CSV.
        var lines = S.Split(new[] { CsvNewLineCharacter }, StringSplitOptions.None);

        // use linq to filter the lines.
        var filteredLines = lines.Where(line =>
        {
            // split each line into individual cells
            var cells = line.Split(CsvCellSeparator);

            // checks each cell in the line to see if it equals "NULL"
            // the line is kept if none of the cells are exactly uppercase "NULL"
            return !cells.Any(cell => cell.Equals(CsvNullCellIdentifier));
        });

        // joins the filtered lines back without the lines that had "NULL" in any cell
        return string.Join(CsvNewLineCharacter.ToString(), filteredLines);
    }

    /// <summary>
    /// Efficiently transforms a CSV string by excluding lines with "NULL" cells.
    /// </summary>
    /// <param name="S">The CSV string to transform.</param>
    /// <remarks>
    /// Similar to SimpleTransformCsv, but uses a more efficient approach for large data sets.
    /// It iterates over the string character by character, avoiding multiple string allocations.
    /// Lines containing "NULL" are skipped, and the remaining lines are concatenated into the result.
    /// </remarks>
    /// <returns>
    /// The transformed CSV string with "NULL" containing lines removed.
    /// </returns>
    public static string TransformCsv(string S)
    {
        // return empty string if input is null or empty
        if (string.IsNullOrEmpty(S))
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();
        var startOfCell = 0;
        var startOfLine = 0;
        var skipLine = false;

        // iterate over each character in the string
        for (var i = 0; i < S.Length; i++)
        {
            // check if current char is a cell separator or end of line
            if (S[i] == CsvCellSeparator || S[i] == CsvNewLineCharacter || i == S.Length - 1)
            {
                // extract cell value
                var cell = S[startOfCell..i];

                // set skip flag if cell contains "NULL"
                if (cell.Equals(CsvNullCellIdentifier))
                {
                    skipLine = true;
                }

                // update start index for next cell
                startOfCell = i + 1;
            }

            // continue loop if not end of line and not end of string
            if (S[i] != CsvNewLineCharacter && i != S.Length - 1) continue;

            // append line to StringBuilder if it doesn't contain "NULL"
            if (!skipLine)
            {
                var endOfLine = (i == S.Length - 1 && S[i] != CsvNewLineCharacter) ? i + 1 : i;
                stringBuilder.Append(S, startOfLine, endOfLine - startOfLine);
                stringBuilder.Append(CsvNewLineCharacter);
            }

            // prepare for next line
            startOfLine = i + 1;
            startOfCell = startOfLine;
            skipLine = false;
        }

        // return processed string, trimming trailing newline character
        return stringBuilder.ToString().TrimEnd(CsvNewLineCharacter);
    }


    /// <summary>
    /// Transforms a CSV string by removing rows that contain the specified "NULL" cell
    /// and ensuring that all cells in a row are not empty.
    /// </summary>
    /// <param name="S">The input CSV string representing data.</param>
    /// <returns>
    /// A transformed CSV formatted string with rows containing "NULL" cells removed
    /// and rows with all empty cells excluded.
    /// </returns>
    /// <remarks>
    /// The assumption is made that fields are not and do not include double-quotes,
    /// and each data "cell" is separated by a comma.
    /// </remarks>
    /// <param name="S">CSV string representing data</param>
    /// <returns>CSV formatted string with expected results.</returns>
    public static string SimpleTransformCsv_NotMine(string S)
    {
        if (string.IsNullOrWhiteSpace(S) is true)
        {
            return string.Empty;
        }

        StringBuilder sb = new(S.Length);

        string[] csv_rows = S.Split(CsvNewLineCharacter);
        if (csv_rows.Length != 0)
        {
            sb.AppendJoin(CsvCellSeparator, csv_rows[0]);
        }

        for (int row = 1; row != csv_rows.Length; row++)
        {
            string[] csv_cells = csv_rows[row].Split(CsvCellSeparator);
            bool all_cells_empty = Array.TrueForAll(csv_cells, string.IsNullOrWhiteSpace);

            if (all_cells_empty is false && Array.IndexOf(csv_cells, CsvNullCellIdentifier) == -1)
            {
                sb.Append(CsvNewLineCharacter);
                sb.AppendJoin(CsvCellSeparator, csv_cells);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Takes CSV string input, parses CSV and returns CSV, where any rows containing specifically "NULL" is removed.
    /// Assumption is made that fields are not and do not include double-quotes and each data "cell" is seperated by a comma.
    /// </summary>
    /// <remarks>
    /// This method leverages the <see cref="IndexedCsvRow"/> struct, which represents a CSV row along with its index.
    /// Once instantiated, that instance of <see cref="IndexedCsvRow"/> is immutable.
    /// <br />
    /// The concept is to provide a structured representation that aims to enhance code reliability and maintainability by
    /// reducing the risk of runtime errors resulting from inadvertent mishandling of data.
    /// <br />
    /// While introducing a layer of abstraction, this approach fosters clarity and protects against potential pitfalls,
    /// serving as a proactive measure to prevent data-related issues within the runtime environment.
    /// <br />
    /// It is important to acknowledge that this approach, while prioritizing code integrity, introduces a performance trade-off
    /// when compared to the more performance-centric methodology showcased in the <see cref="SimpleTransformCsv"/> method.
    /// <br />
    /// In certain scenarios, optimal performance may not always be the sole determinant of overall success.
    /// </remarks>
    /// <param name="S">CSV string representing data</param>
    /// <returns>CSV formatted string with expected results.</returns>
    public static string TransformCsv_NotMine(string S)
    {
        if (string.IsNullOrWhiteSpace(S) is true)
        {
            return string.Empty;
        }

        StringBuilder sb = new();

        // split each each line by new line character `CsvNewLineCharacter` and then by `CsvCellSeparator` to get a list of strings giving us rows of cells.
        IEnumerable<string[]> csv_cell_rows = S.Split(CsvNewLineCharacter)
            .Select(csv_line => csv_line.Split(CsvCellSeparator));

        // index our data, so we know that the first row is always headers, and access this first row when we need to
        IList<IndexedCsvRow> indexed_csv_cell_rows = csv_cell_rows.Select(IndexedCsvRow.Create).ToList();

        foreach (IndexedCsvRow csv_row in indexed_csv_cell_rows)
        {
            // First row > headers > continue
            if (csv_row.Index == 0)
            {
                sb.AppendJoin(CsvCellSeparator, csv_row.Row);
                continue;
            }

            // If row does not contain NULL - add to our returning data - The method IList.Contains() by default IS case-sensitive!
            if (csv_row.Row.Contains(CsvNullCellIdentifier) == false && csv_row.Row.All(string.IsNullOrEmpty) == false)
            {
                sb.Append(CsvNewLineCharacter);
                sb.AppendJoin(CsvCellSeparator, csv_row.Row);
            }
        }

        string results = sb.ToString();
        return results;
    }
}