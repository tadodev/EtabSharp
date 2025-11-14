namespace EtabSharp.Loads.LoadCombos.Models;

/// <summary>
/// Represents a load combination in ETABS.
/// Load combinations are linear combinations of load cases used for design and analysis.
/// </summary>
public class LoadCombination
{
    /// <summary>
    /// Name of the load combination
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Type of combination (0=Linear Add, 1=Envelope, 2=Absolute Add, 3=SRSS, 4=Range Add)
    /// </summary>
    public int ComboType { get; set; }

    /// <summary>
    /// List of cases included in this combination
    /// </summary>
    public List<LoadCombinationCase> Cases { get; set; } = new();

    /// <summary>
    /// Notes or description for the combination
    /// </summary>
    public string Notes { get; set; } = "";

    /// <summary>
    /// Whether this is a design combination
    /// </summary>
    public bool IsDesignCombo { get; set; }

    /// <summary>
    /// Gets the total number of cases in this combination
    /// </summary>
    public int CaseCount => Cases.Count;

    /// <summary>
    /// Adds a load case to the combination
    /// </summary>
    public void AddCase(LoadCombinationCase comboCase)
    {
        if (comboCase != null)
        {
            Cases.Add(comboCase);
        }
    }

    /// <summary>
    /// Removes a load case from the combination
    /// </summary>
    public bool RemoveCase(string caseName)
    {
        return Cases.RemoveAll(c => c.CaseName == caseName) > 0;
    }

    /// <summary>
    /// Gets combo type description
    /// </summary>
    public string GetComboTypeDescription()
    {
        return ComboType switch
        {
            0 => "Linear Add",
            1 => "Envelope",
            2 => "Absolute Add",
            3 => "SRSS (Square Root Sum of Squares)",
            4 => "Range Add",
            _ => $"Unknown ({ComboType})"
        };
    }

    public override string ToString()
    {
        return $"Combo '{Name}': {GetComboTypeDescription()} with {CaseCount} cases";
    }
}