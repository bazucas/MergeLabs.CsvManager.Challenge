using System.Text;

namespace MergeLabs.CsvManager.Lib;

public class CsvManager
{
    private const char CsvNewLineCharacter = '\n';
    private const char CsvCellSeparator = ','; // seperator typo
    private const string CsvNullCellIdentifier = "NULL";

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

    public static string TransformCsv(string S)
    {
        if (string.IsNullOrEmpty(S))
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();
        var startOfLine = 0;

        for (var i = 0; i < S.Length; i++)
        {
            var lineIsValid = true;

            if (S[i] == CsvNewLineCharacter || i == S.Length - 1)
            {
                var endOfLine = (i == S.Length - 1) ? i + 1 : i;

                for (var j = startOfLine; j < endOfLine; j++)
                {
                    if (S.Substring(j, Math.Min(CsvNullCellIdentifier.Length, endOfLine - j)).Equals(CsvNullCellIdentifier))
                    {
                        lineIsValid = false;
                        break;
                    }
                }

                if (lineIsValid)
                {
                    stringBuilder.Append(S, startOfLine, endOfLine - startOfLine);
                    if (i != S.Length - 1)
                    {
                        stringBuilder.Append(CsvNewLineCharacter);
                    }
                }

                startOfLine = i + 1;
            }
        }

        return stringBuilder.ToString();
    }
}