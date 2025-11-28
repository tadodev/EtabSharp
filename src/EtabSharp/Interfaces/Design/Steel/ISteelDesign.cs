using EtabSharp.Design.Steel.Models;
using EtabSharp.Interfaces.Design.Steel.Codes;
using ETABSv1;

namespace EtabSharp.Interfaces.Design.Steel;

/// <summary>
/// Main interface for steel design operations in ETABS.
/// Provides access to different steel design codes and common design operations across all codes.
/// Maps to cDesignSteel interface in ETABS API.
/// </summary>
public interface ISteelDesign
{
    #region Code Management

    /// <summary>
    /// Gets the currently active steel design code name.
    /// Wraps cDesignSteel.GetCode.
    /// </summary>
    /// <returns>Name of the current steel design code</returns>
    string GetCode();

    /// <summary>
    /// Sets the active steel design code.
    /// Wraps cDesignSteel.SetCode.
    /// </summary>
    /// <param name="codeName">Name of the steel design code to activate</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetCode(string codeName);

    #endregion

    #region Design Section Management

    /// <summary>
    /// Gets the design section assigned to a frame.
    /// Wraps cDesignSteel.GetDesignSection.
    /// </summary>
    /// <param name="frameName">Name of frame object</param>
    /// <returns>Name of the assigned design section</returns>
    string GetDesignSection(string frameName);

    /// <summary>
    /// Sets the design section for a frame.
    /// Wraps cDesignSteel.SetDesignSection.
    /// </summary>
    /// <param name="name">Name of frame object or group</param>
    /// <param name="propName">Name of the section to assign</param>
    /// <param name="lastAnalysis">If true, resets to last analysis section</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDesignSection(string name, string propName, bool lastAnalysis = false, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a frame or group to auto-select its design section.
    /// Wraps cDesignSteel.SetAutoSelectNull.
    /// </summary>
    /// <param name="name">Name of frame object or group</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAutoSelectNull(string name, eItemType itemType = eItemType.Objects);

    #endregion

    #region Design Execution

    /// <summary>
    /// Starts steel design process.
    /// Wraps cDesignSteel.StartDesign.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int StartDesign();

    /// <summary>
    /// Resets all design overwrites to program defaults.
    /// Wraps cDesignSteel.ResetOverwrites.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ResetOverwrites();

    /// <summary>
    /// Deletes all steel design results.
    /// Wraps cDesignSteel.DeleteResults.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteResults();

    /// <summary>
    /// Checks if design results are available.
    /// Wraps cDesignSteel.GetResultsAvailable.
    /// </summary>
    /// <returns>True if design results are available, false otherwise</returns>
    bool GetResultsAvailable();

    #endregion

    #region Design Summary Results

    /// <summary>
    /// Gets steel design summary results (basic version).
    /// Wraps cDesignSteel.GetSummaryResults.
    /// </summary>
    /// <param name="name">Name of frame object, group, or empty string for all</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>SteelDesignSummaryResults containing design check results</returns>
    SteelDesignSummaryResults GetSummaryResults(string name = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets steel design summary results with detailed information (latest version).
    /// Wraps cDesignSteel.GetSummaryResults_3.
    /// </summary>
    /// <param name="name">Name of frame object, group, or empty string for all</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>SteelDesignSummaryResults containing detailed design check results</returns>
    SteelDesignSummaryResults GetSummaryResults_3(string name = "", eItemType itemType = eItemType.Objects);

    #endregion

    #region Verification

    /// <summary>
    /// Verifies passed frames in design.
    /// Wraps cDesignSteel.VerifyPassed.
    /// </summary>
    /// <returns>Tuple containing (NumberItems, N1, N2, FrameNames[])</returns>
    (int NumberItems, int N1, int N2, string[] FrameNames) VerifyPassed();

    /// <summary>
    /// Verifies sections in design.
    /// Wraps cDesignSteel.VerifySections.
    /// </summary>
    /// <returns>Tuple containing (NumberItems, SectionNames[])</returns>
    (int NumberItems, string[] SectionNames) VerifySections();

    #endregion

    #region Load Combinations for Design

    /// <summary>
    /// Gets load combinations selected for deflection design.
    /// Wraps cDesignSteel.GetComboDeflection.
    /// </summary>
    /// <returns>Array of load combination names</returns>
    string[] GetComboDeflection();

    /// <summary>
    /// Gets load combinations selected for strength design.
    /// Wraps cDesignSteel.GetComboStrength.
    /// </summary>
    /// <returns>Array of load combination names</returns>
    string[] GetComboStrength();

    /// <summary>
    /// Sets whether a load combination is selected for deflection design.
    /// Wraps cDesignSteel.SetComboDeflection.
    /// </summary>
    /// <param name="comboName">Name of load combination</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetComboDeflection(string comboName, bool selected);

    /// <summary>
    /// Sets whether a load combination is selected for strength design.
    /// Wraps cDesignSteel.SetComboStrength.
    /// </summary>
    /// <param name="comboName">Name of load combination</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetComboStrength(string comboName, bool selected);

    #endregion

    #region Design Groups

    /// <summary>
    /// Gets groups selected for design.
    /// Wraps cDesignSteel.GetGroup.
    /// </summary>
    /// <returns>Array of group names</returns>
    string[] GetGroup();

    /// <summary>
    /// Sets whether a group is selected for design.
    /// Wraps cDesignSteel.SetGroup.
    /// </summary>
    /// <param name="groupName">Name of group</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroup(string groupName, bool selected);

    #endregion

    #region Target Displacement and Period

    /// <summary>
    /// Gets target displacement parameters.
    /// Wraps cDesignSteel.GetTargetDispl.
    /// </summary>
    /// <returns>Tuple containing (NumberItems, LoadCases[], Points[], Displacements[], Active)</returns>
    (int NumberItems, string[] LoadCases, string[] Points, double[] Displacements, bool Active) GetTargetDispl();

    /// <summary>
    /// Sets target displacement parameters.
    /// Wraps cDesignSteel.SetTargetDispl.
    /// </summary>
    /// <param name="loadCases">Array of load case names</param>
    /// <param name="points">Array of point names</param>
    /// <param name="displacements">Array of displacement values</param>
    /// <param name="active">True to activate target displacement</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTargetDispl(string[] loadCases, string[] points, double[] displacements, bool active = true);

    /// <summary>
    /// Gets target period parameters.
    /// Wraps cDesignSteel.GetTargetPeriod.
    /// </summary>
    /// <returns>Tuple containing (NumberItems, ModalCase, Modes[], Periods[], Active)</returns>
    (int NumberItems, string ModalCase, int[] Modes, double[] Periods, bool Active) GetTargetPeriod();

    /// <summary>
    /// Sets target period parameters.
    /// Wraps cDesignSteel.SetTargetPeriod.
    /// </summary>
    /// <param name="modalCase">Name of modal load case</param>
    /// <param name="modes">Array of mode numbers</param>
    /// <param name="periods">Array of period values</param>
    /// <param name="active">True to activate target period</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTargetPeriod(string modalCase, int[] modes, double[] periods, bool active = true);

    #endregion

    #region Code-Specific Interfaces

    /// <summary>
    /// Gets the AISC 360-16 steel design interface.
    /// </summary>
    IAISC360_16 AISC360_16 { get; }

    #endregion
}