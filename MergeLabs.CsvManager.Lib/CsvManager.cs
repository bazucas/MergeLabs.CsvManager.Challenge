using System.Text;

namespace MergeLabs.CsvManager.Lib;

public class CsvManager
{
    private const char CsvNewLineCharacter = '\n';
    private const char CsvCellSeparator = ','; // seperator typo
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
}