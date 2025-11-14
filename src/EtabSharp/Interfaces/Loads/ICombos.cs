using EtabSharp.Loads.LoadCombos.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Loads;

/// <summary>
/// Provides methods for managing load combinations in the ETABS model.
/// Load combinations define how load cases are combined for analysis and design.
/// </summary>
public interface ICombos
{
    #region Combination Creation & Management

    /// <summary>
    /// Adds a new load combination to the model.
    /// </summary>
    /// <param name="name">Name of the load combination</param>
    /// <param name="comboType">Type of combination (0=Linear Add, 1=Envelope, 2=Absolute Add, 3=SRSS, 4=Range Add)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Add(string name, int comboType);

    /// <summary>
    /// Adds a new load combination using the enum type.
    /// </summary>
    /// <param name="name">Name of the load combination</param>
    /// <param name="comboType">Type of combination</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Add(string name, LoadComboType comboType);

    /// <summary>
    /// Adds default design load combinations for specified design types.
    /// </summary>
    /// <param name="designSteel">Include steel design combinations</param>
    /// <param name="designConcrete">Include concrete design combinations</param>
    /// <param name="designAluminum">Include aluminum design combinations</param>
    /// <param name="designColdFormed">Include cold-formed steel design combinations</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddDesignDefaultCombos(bool designSteel, bool designConcrete, bool designAluminum, bool designColdFormed);

    /// <summary>
    /// Adds default design combinations using options object.
    /// </summary>
    /// <param name="options">Design combo options</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddDesignDefaultCombos(DesignComboOptions options);

    /// <summary>
    /// Deletes a load combination from the model.
    /// </summary>
    /// <param name="name">Name of the combination to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string name);

    /// <summary>
    /// Gets the type of a load combination.
    /// </summary>
    /// <param name="name">Name of the combination</param>
    /// <returns>Combination type (0=Linear Add, 1=Envelope, 2=Absolute Add, 3=SRSS, 4=Range Add)</returns>
    int GetComboType(string name);

    /// <summary>
    /// Retrieves the names of all defined load combinations.
    /// </summary>
    /// <returns>Array of combination names</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets the count of load combinations in the model.
    /// </summary>
    /// <returns>Number of combinations</returns>
    int Count();

    #endregion

    #region Case Management within Combinations

    /// <summary>
    /// Adds or modifies a load case/combo within a combination.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <param name="caseType">Type of case (LoadCase or LoadCombo)</param>
    /// <param name="caseName">Name of the load case or combination</param>
    /// <param name="scaleFactor">Scale factor to apply</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetCaseList(string comboName, eCNameType caseType, string caseName, double scaleFactor);

    /// <summary>
    /// Adds or modifies a load case/combo with mode number support.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <param name="caseType">Type of case (LoadCase or LoadCombo)</param>
    /// <param name="caseName">Name of the load case or combination</param>
    /// <param name="modeNumber">Mode number (for modal cases, 0 if not applicable)</param>
    /// <param name="scaleFactor">Scale factor to apply</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetCaseList(string comboName, eCNameType caseType, string caseName, int modeNumber, double scaleFactor);

    /// <summary>
    /// Gets the list of cases in a load combination.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <returns>List of LoadCombinationCase objects</returns>
    List<LoadCombinationCase> GetCaseList(string comboName);

    /// <summary>
    /// Deletes a specific case from a load combination.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <param name="caseType">Type of case to delete</param>
    /// <param name="caseName">Name of the case to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteCase(string comboName, eCNameType caseType, string caseName);

    #endregion

    #region Complete Combination Queries

    /// <summary>
    /// Gets a complete load combination with all its cases.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <returns>LoadCombination model with all details</returns>
    LoadCombination GetLoadCombination(string comboName);

    /// <summary>
    /// Gets all load combinations in the model.
    /// </summary>
    /// <returns>List of LoadCombination models</returns>
    List<LoadCombination> GetAllCombinations();

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Creates a simple linear combination with specified cases.
    /// </summary>
    /// <param name="name">Name of the combination</param>
    /// <param name="cases">Array of (caseName, scaleFactor) tuples</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateLinearCombo(string name, params (string caseName, double factor)[] cases);

    /// <summary>
    /// Creates an envelope combination.
    /// </summary>
    /// <param name="name">Name of the combination</param>
    /// <param name="caseNames">Names of cases to include in envelope</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateEnvelopeCombo(string name, params string[] caseNames);

    /// <summary>
    /// Creates a typical dead + live combination.
    /// </summary>
    /// <param name="name">Name of the combination</param>
    /// <param name="deadFactor">Dead load factor</param>
    /// <param name="liveFactor">Live load factor</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateDeadPlusLive(string name, double deadFactor = 1.0, double liveFactor = 1.0);

    /// <summary>
    /// Adds a load case to an existing combination.
    /// </summary>
    /// <param name="comboName">Name of the combination</param>
    /// <param name="caseName">Name of the load case</param>
    /// <param name="scaleFactor">Scale factor</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddLoadCaseToCombo(string comboName, string caseName, double scaleFactor = 1.0);

    /// <summary>
    /// Adds a load combination to an existing combination.
    /// </summary>
    /// <param name="comboName">Name of the parent combination</param>
    /// <param name="nestedComboName">Name of the combination to add</param>
    /// <param name="scaleFactor">Scale factor</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddLoadComboToCombo(string comboName, string nestedComboName, double scaleFactor = 1.0);

    /// <summary>
    /// Checks if a combination exists.
    /// </summary>
    /// <param name="name">Name of the combination</param>
    /// <returns>True if exists, false otherwise</returns>
    bool Exists(string name);

    /// <summary>
    /// Gets combinations that include a specific load case.
    /// </summary>
    /// <param name="caseName">Name of the load case</param>
    /// <returns>List of combination names that use this case</returns>
    List<string> GetCombosUsingCase(string caseName);

    #endregion
}