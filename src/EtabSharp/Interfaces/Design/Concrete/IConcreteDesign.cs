using EtabSharp.Design.Concrete.Models;
using EtabSharp.Interfaces.Design.Concrete.Codes;
using ETABSv1;

namespace EtabSharp.Interfaces.Design.Concrete;

/// <summary>
/// Main interface for concrete design operations in ETABS.
/// Provides access to different concrete design codes and common design operations across all codes.
/// Maps to cDesignConcrete interface in ETABS API.
/// </summary>
public interface IConcreteDesign
{
    #region Code Management

    /// <summary>
    /// Gets the currently active concrete design code name.
    /// Wraps cDesignConcrete.GetCode.
    /// </summary>
    string GetCode();

    /// <summary>
    /// Sets the active concrete design code.
    /// Wraps cDesignConcrete.SetCode.
    /// </summary>
    int SetCode(string codeName);

    #endregion

    #region Design Section Management

    /// <summary>
    /// Gets the design section assigned to a frame.
    /// Wraps cDesignConcrete.GetDesignSection.
    /// </summary>
    string GetDesignSection(string frameName);

    /// <summary>
    /// Sets the design section for a frame.
    /// Wraps cDesignConcrete.SetDesignSection.
    /// </summary>
    int SetDesignSection(string name, string propName, bool lastAnalysis = false, eItemType itemType = eItemType.Objects);

    #endregion

    #region Design Execution

    /// <summary>
    /// Starts concrete design process.
    /// Wraps cDesignConcrete.StartDesign.
    /// </summary>
    int StartDesign();

    /// <summary>
    /// Checks if design results are available.
    /// Wraps cDesignConcrete.GetResultsAvailable.
    /// </summary>
    bool GetResultsAvailable();

    #endregion

    #region Design Summary Results

    /// <summary>
    /// Gets concrete beam design summary results.
    /// Wraps cDesignConcrete.GetSummaryResultsBeam.
    /// </summary>
    ConcreteBeamDesignResults GetSummaryResultsBeam(string name = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets concrete beam design summary results with detailed information.
    /// Wraps cDesignConcrete.GetSummaryResultsBeam_2.
    /// </summary>
    ConcreteBeamDesignResults GetSummaryResultsBeam_2(string name = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets concrete column design summary results.
    /// Wraps cDesignConcrete.GetSummaryResultsColumn.
    /// </summary>
    ConcreteColumnDesignResults GetSummaryResultsColumn(string name = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets concrete joint design summary results.
    /// Wraps cDesignConcrete.GetSummaryResultsJoint.
    /// </summary>
    ConcreteJointDesignResults GetSummaryResultsJoint(string name = "", eItemType itemType = eItemType.Objects);

    #endregion

    #region Load Combinations for Design

    /// <summary>
    /// Sets whether a load combination is selected for strength design.
    /// Wraps cDesignConcrete.SetComboStrength.
    /// </summary>
    int SetComboStrength(string comboName, bool selected);

    #endregion

    #region Rebar Preferences

    /// <summary>
    /// Gets rebar preferences for beams.
    /// Wraps cDesignConcrete.GetRebarPrefsBeam.
    /// </summary>
    string GetRebarPrefsBeam(int item);

    /// <summary>
    /// Gets rebar preferences for columns.
    /// Wraps cDesignConcrete.GetRebarPrefsColumn.
    /// </summary>
    string GetRebarPrefsColumn(int item);

    #endregion

    #region Seismic Framing Type

    /// <summary>
    /// Gets seismic framing type for frames.
    /// Wraps cDesignConcrete.GetSeismicFramingType.
    /// </summary>
    (int NumberItems, string[] FrameNames, int[] FramingTypes) GetSeismicFramingType(string name = "", eItemType itemType = eItemType.Objects);

    #endregion

    #region Code-Specific Interfaces

    /// <summary>
    /// Gets the ACI 318-14 concrete design interface.
    /// </summary>
    IACI318_14 ACI318_14 { get; }

    #endregion
}