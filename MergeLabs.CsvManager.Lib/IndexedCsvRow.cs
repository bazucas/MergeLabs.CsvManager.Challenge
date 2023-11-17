using System.Collections.Immutable;

namespace MergeLabs.CsvManager.Lib;

/// <summary>
/// Represents a row of a CSV data along with its index.
/// </summary>
public readonly struct IndexedCsvRow
{
    /// <summary>
    /// Gets the collection of strings representing the CSV row.
    /// </summary>
    public readonly IImmutableList<string> Row { get; private init; }

    /// <summary>
    /// Gets the index of the CSV row.
    /// </summary>
    public readonly int Index { get; private init; }

    /// <summary>
    /// Private constructor to create an instance of the <see cref="IndexedCsvRow"/> struct.
    /// </summary>
    /// <param name="row">An array of strings representing the CSV row.</param>
    /// <param name="index">The index of the CSV row.</param>
    private IndexedCsvRow(string[] row, int index)
    {
        Row = row.ToImmutableList();
        Index = index;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="IndexedCsvRow"/> struct.
    /// </summary>
    /// <param name="row">An array of strings representing the CSV row.</param>
    /// <param name="idx">The index of the CSV row.</param>
    /// <returns>An instance of the <see cref="IndexedCsvRow"/> struct.</returns>
    public static IndexedCsvRow Create(string[] row, int idx)
        => new(row, idx);
}