using EtabSharp.Loads.LoadCases.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Loads;

/// <summary>
/// Interface for managing load cases in the ETABS model.
/// Load cases define how load patterns are combined and analyzed.
/// </summary>
public interface ILoadCases
{
    #region Load Case Management

    /// <summary>
    /// Changes the name of an existing load case.
    /// Wraps cSapModel.LoadCases.ChangeName.
    /// </summary>
    /// <param name="currentName">Current name of the load case</param>
    /// <param name="newName">New name for the load case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Deletes a load case from the model.
    /// Wraps cSapModel.LoadCases.Delete.
    /// </summary>
    /// <param name="name">Name of the load case to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string name);

    /// <summary>
    /// Gets the count of load cases in the model.
    /// Wraps cSapModel.LoadCases.Count.
    /// </summary>
    /// <param name="caseType">Type of load cases to count (0 for all types)</param>
    /// <returns>Total number of load cases of the specified type</returns>
    int Count(eLoadCaseType caseType = 0);

    /// <summary>
    /// Gets the names of all load cases in the model.
    /// Wraps cSapModel.LoadCases.GetNameList.
    /// </summary>
    /// <param name="caseType">Type of load cases to retrieve (0 for all types)</param>
    /// <returns>Array of load case names</returns>
    string[] GetNameList(eLoadCaseType caseType = 0);

    #endregion

    #region Load Case Properties

    /// <summary>
    /// Gets the type and subtype of a load case.
    /// Wraps cSapModel.LoadCases.GetTypeOAPI.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <returns>Tuple of (CaseType, SubType)</returns>
    (eLoadCaseType CaseType, int SubType) GetTypeOAPI(string name);

    /// <summary>
    /// Gets complete information about a load case.
    /// Wraps cSapModel.LoadCases.GetTypeOAPI_1.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <returns>Tuple of (CaseType, SubType, DesignType, DesignTypeOption, IsAuto)</returns>
    (eLoadCaseType CaseType, int SubType, eLoadPatternType DesignType, int DesignTypeOption, bool IsAuto) GetTypeOAPI_1(string name);

    /// <summary>
    /// Sets the design type for a load case.
    /// Wraps cSapModel.LoadCases.SetDesignType.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <param name="designTypeOption">Design type option (1=Strength, 2=Serviceability, 3=Other)</param>
    /// <param name="designType">Design type (load pattern type)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDesignType(string name, int designTypeOption, eLoadPatternType designType = eLoadPatternType.Dead);

    /// <summary>
    /// Gets a complete load case with all properties.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <returns>LoadCase model with all properties</returns>
    LoadCase GetLoadCase(string name);

    /// <summary>
    /// Gets all load cases in the model with their properties.
    /// </summary>
    /// <param name="caseType">Type of load cases to retrieve (0 for all types)</param>
    /// <returns>List of all LoadCase objects</returns>
    List<LoadCase> GetAllLoadCases(eLoadCaseType caseType = 0);

    #endregion

    #region Specific Load Case Type Managers

    /// <summary>
    /// Gets the static linear load case manager.
    /// Wraps cSapModel.LoadCases.StaticLinear.
    /// </summary>
    cCaseStaticLinear StaticLinear { get; }

    /// <summary>
    /// Gets the static nonlinear load case manager.
    /// Wraps cSapModel.LoadCases.StaticNonlinear.
    /// </summary>
    cCaseStaticNonlinear StaticNonlinear { get; }

    /// <summary>
    /// Gets the static nonlinear staged load case manager.
    /// Wraps cSapModel.LoadCases.StaticNonlinearStaged.
    /// </summary>
    cCaseStaticNonlinearStaged StaticNonlinearStaged { get; }

    /// <summary>
    /// Gets the modal eigen load case manager.
    /// Wraps cSapModel.LoadCases.ModalEigen.
    /// </summary>
    cCaseModalEigen ModalEigen { get; }

    /// <summary>
    /// Gets the modal ritz load case manager.
    /// Wraps cSapModel.LoadCases.ModalRitz.
    /// </summary>
    cCaseModalRitz ModalRitz { get; }

    /// <summary>
    /// Gets the response spectrum load case manager.
    /// Wraps cSapModel.LoadCases.ResponseSpectrum.
    /// </summary>
    cCaseResponseSpectrum ResponseSpectrum { get; }

    /// <summary>
    /// Gets the modal history linear load case manager.
    /// Wraps cSapModel.LoadCases.ModHistLinear.
    /// </summary>
    cCaseModalHistoryLinear ModHistLinear { get; }

    /// <summary>
    /// Gets the modal history nonlinear load case manager.
    /// Wraps cSapModel.LoadCases.ModHistNonlinear.
    /// </summary>
    cCaseModalHistoryNonlinear ModHistNonlinear { get; }

    /// <summary>
    /// Gets the direct history linear load case manager.
    /// Wraps cSapModel.LoadCases.DirHistLinear.
    /// </summary>
    cCaseDirectHistoryLinear DirHistLinear { get; }

    /// <summary>
    /// Gets the direct history nonlinear load case manager.
    /// Wraps cSapModel.LoadCases.DirHistNonlinear.
    /// </summary>
    cCaseDirectHistoryNonlinear DirHistNonlinear { get; }

    /// <summary>
    /// Gets the buckling load case manager.
    /// Wraps cSapModel.LoadCases.Buckling.
    /// </summary>
    cCaseBuckling Buckling { get; }

    /// <summary>
    /// Gets the hyperstatic load case manager.
    /// Wraps cSapModel.LoadCases.HyperStatic.
    /// </summary>
    cCaseHyperStatic HyperStatic { get; }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Checks if a load case exists in the model.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <returns>True if the case exists, false otherwise</returns>
    bool LoadCaseExists(string name);

    /// <summary>
    /// Gets load cases by type.
    /// </summary>
    /// <param name="caseType">Type of load cases to retrieve</param>
    /// <returns>List of load case names of the specified type</returns>
    List<string> GetLoadCasesByType(eLoadCaseType caseType);

    /// <summary>
    /// Gets all static load cases (linear and nonlinear).
    /// </summary>
    /// <returns>List of static load case names</returns>
    List<string> GetStaticLoadCases();

    /// <summary>
    /// Gets all dynamic load cases (modal, response spectrum, time history).
    /// </summary>
    /// <returns>List of dynamic load case names</returns>
    List<string> GetDynamicLoadCases();

    /// <summary>
    /// Gets all nonlinear load cases.
    /// </summary>
    /// <returns>List of nonlinear load case names</returns>
    List<string> GetNonlinearLoadCases();

    /// <summary>
    /// Gets all time history load cases.
    /// </summary>
    /// <returns>List of time history load case names</returns>
    List<string> GetTimeHistoryLoadCases();

    /// <summary>
    /// Gets all auto-generated load cases.
    /// </summary>
    /// <returns>List of auto-generated load case names</returns>
    List<string> GetAutoGeneratedLoadCases();

    /// <summary>
    /// Gets a summary of all load cases in the model.
    /// </summary>
    /// <returns>LoadCaseSummary with statistics</returns>
    LoadCaseSummary GetLoadCaseSummary();

    /// <summary>
    /// Deletes all load cases of a specific type.
    /// </summary>
    /// <param name="caseType">Type of load cases to delete</param>
    /// <returns>Number of cases deleted</returns>
    int DeleteLoadCasesByType(eLoadCaseType caseType);

    /// <summary>
    /// Sets design type for multiple load cases.
    /// </summary>
    /// <param name="caseNames">Array of load case names</param>
    /// <param name="designTypeOption">Design type option (1=Strength, 2=Serviceability, 3=Other)</param>
    /// <param name="designType">Design type (load pattern type)</param>
    /// <returns>Number of cases successfully updated</returns>
    int SetDesignTypeMultiple(string[] caseNames, int designTypeOption, eLoadPatternType designType);

    /// <summary>
    /// Gets load cases by design type option.
    /// </summary>
    /// <param name="designTypeOption">Design type option (1=Strength, 2=Serviceability, 3=Other)</param>
    /// <returns>List of load case names with the specified design type option</returns>
    List<string> GetLoadCasesByDesignType(int designTypeOption);

    #endregion
}